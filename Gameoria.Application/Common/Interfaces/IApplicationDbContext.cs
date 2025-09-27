using GameOria.Domains.Entities.Orders;
using GameOria.Domains.Entities.Games;
using GameOria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;


//IApplicationDbContext: للتعامل مع قاعدة البيانات

namespace GameOria.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
       // DbSet<Game> Games { get; }
        //DbSet<GameCode> GameCodes { get; }
        ////DbSet<GameAccount> GameAccounts { get; }
        //DbSet<GameAccount> GameAccounts { get; }

        //DbSet<Store> Stores { get; }
        //DbSet<Order> Orders { get; }
        //DbSet<OrderItem> OrderItems { get; }
        //DbSet<GameReview> Reviews { get; }
        //DbSet<GameCategory> GameCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
