using GameOria.Infrastructure.Helper.Model;
using GameOria.Web.Service.Handlers;
using GameOria.Web.Service.Implementation;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Configurations --------------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddAutoMapper(typeof(Program));

// -------------------- HttpClients --------------------
builder.Services.AddHttpClient<IAuthService, AuthService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7075/api/Auth/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<IOrganizerService, OrganizerService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7075/api/OrganizerAPI/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient("GameOriaApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:7075/api/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

// -------------------- JWT Authentication --------------------
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // read token from cookie
            var token = context.HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
                context.Token = token;

            return Task.CompletedTask;
        }
    };

});
// -------------------- Build --------------------
var app = builder.Build();

// -------------------- Middleware Pipeline --------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
