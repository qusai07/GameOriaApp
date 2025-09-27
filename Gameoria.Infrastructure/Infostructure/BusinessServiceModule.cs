using GameOria.Api.Helper.Service;
using GameOria.Api.Repo.Interface;
using GameOria.Application.Common.Interfaces;
using GameOria.Domains.Entities.Identity;
using GameOria.Infrastructure.Data;
using GameOria.Infrastructure.Repo.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GameOria.Infrastructure.Infastructure
{
    public static class BusinessServiceModule
    {
        public static void AddBusinessServiceModule(this IServiceCollection Services)
        {
            Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            Services.AddScoped<JwtHelper>();

            Services.AddScoped<IDataService, DataService>();
            Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<GameOriaDbContext>());


        }
    }
}
