using GameOria.Application.Stores.Service;
using GameOria.Web.Service.Handlers;
using GameOria.Web.Service.Implementation;
using GameOria.Web.Service.Interface;
using System.Net.Http;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();   // Save Session in meomery
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);     // expiration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthHeaderHandler>();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddHttpClient<IAuthService, AuthService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7075/api/Auth/");
})
.AddHttpMessageHandler<AuthHeaderHandler>();



builder.Services.AddHttpClient<IOrganizerService, OrganizerService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7075/api/OrganizerAPI/");
});
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddHttpClient("GameOriaApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:7075/api/");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
