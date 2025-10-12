using GameOria.Api.StartUp;
using GameOria.Infrastructure.Data;
using GameOria.Infrastructure.Helper.Model;
using GameOria.Infrastructure.Helper.Service;

var builder = WebApplication.CreateBuilder(args);


//  Add core framework services

builder.Services.AddCorsPolicies();
builder.Services.AddControllers();

// ---------------------------

//  Configure strongly typed settings for JWT & Email

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


//  Register custom application services

builder.Services.AddScoped<JwtHelper>();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();

// ---------------------------

//  Authentication & Authorization setup

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// ---------------------------

// Add Swagger (API Docs)

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

// ---------------------------
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalMiddlewares();
app.MapControllers();

app.Run();