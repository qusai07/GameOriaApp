var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Kestrel to use development certificates
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient("UnsafeHttpClient")
         .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
         {
             ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
         });
}

// Add authentication and authorization services
builder.Services.AddAuthentication()
    .AddCookie();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();