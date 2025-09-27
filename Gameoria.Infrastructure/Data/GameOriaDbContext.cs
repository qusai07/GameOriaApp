
using GameOria.Application.Common.Interfaces;
using GameOria.Domains.Entities.Games;
using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Entities.Orders;
using GameOria.Domains.Entities.Stores;
using GameOria.Domains.Entities.Users;
using GameOria.Domains.ValueObjects;
using GameOria.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GameOria.Infrastructure.Data
{
    public class GameOriaDbContext : DbContext, IApplicationDbContext
    {
        public GameOriaDbContext(DbContextOptions<GameOriaDbContext> options)
                : base(options)
        { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<OrganizerUser> OrganizerUsers { get; set; } = null!;
        public DbSet<CustomerUser> CustomerUsers { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }

}
    
