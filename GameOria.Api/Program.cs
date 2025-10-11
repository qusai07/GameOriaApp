using GameOria.Api.StartUp;
using GameOria.Infrastructure.Data;
using GameOria.Infrastructure.Helper.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCorsPolicies();
builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.Configure<EmailSettings>(
           builder.Configuration.GetSection("EmailSettings"));
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add authentication and authorization services
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
var app = builder.Build();
//DatabaseInitializer.InitializeDatabase(app.Services);

app.UseGlobalMiddlewares();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();