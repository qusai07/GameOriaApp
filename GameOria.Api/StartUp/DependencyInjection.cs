
using GameOria.Infrastructure.Data;
using GameOria.Infrastructure.Infastructure;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Api.StartUp
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddBusinessServiceModule(); 
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameOriaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalNetwork", policy =>
                {
                    policy.WithOrigins("https://10.255.254.40.2:7075", "http://localhost:7075")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}

