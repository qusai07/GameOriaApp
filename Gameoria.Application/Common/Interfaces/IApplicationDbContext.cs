using Gameoria.Application.Features.Games.Common;
using Gameoria.Domains.Entities.Games;
using Gameoria.Domains.Entities.Orders;
using Gameoria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;


//IApplicationDbContext: للتعامل مع قاعدة البيانات

namespace Gameoria.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Game> Games { get; }
        DbSet<GameCodeDto> GameCodes { get; }
        DbSet<GameAccountDto> GameAccounts { get; }
        DbSet<Store> Stores { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<GameReview> Reviews { get; }
        DbSet<GameCategory> Category { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
