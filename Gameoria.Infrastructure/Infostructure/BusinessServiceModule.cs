using GameOria.Api.Helper.Service;
using GameOria.Api.Repo.Interface;
using GameOria.Application.Common.Interfaces;
using GameOria.Application.Interface;
using GameOria.Application.Orders.Service;
using GameOria.Application.Stores.Service;
using GameOria.Domains.Entities.Identity;
using GameOria.Infrastructure.Data;
using GameOria.Infrastructure.Helper.Model;
using GameOria.Infrastructure.Implementations;
using GameOria.Infrastructure.Repositories;
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
            Services.AddScoped<IMailService, MailService>();
          

            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IOrderCodeRepository, OrderCodeRepository>();
            Services.AddScoped<IOrderItemRepository, OrderItemRepository>();



            Services.AddScoped<IStoreReviewRepository, StoreReviewRepository>();
            Services.AddScoped<IStoreRepository, StoreRepository>();



        }
    }
}
