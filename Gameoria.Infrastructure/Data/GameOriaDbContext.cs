
using GameOria.Application.Common.Interfaces;
using GameOria.Domains.Entities.Cards;
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
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreOwner> StoreOwners { get; set; }
        public DbSet<StoreReview> StoreReviews { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GameCode> GameCodes { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardCategory> CardCategories { get; set; }
        public DbSet<CardCode> CardCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderCode> OrderCodes { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            builder.Entity<AdminUser>().ToTable("AdminUsers");
            builder.Entity<OrganizerUser>().ToTable("OrganizerUsers");

            builder.ApplyConfiguration(new CardConfiguration());
            builder.ApplyConfiguration(new GameConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
            builder.ApplyConfiguration(new StoreConfiguration());
        

        // تعليق هذا السطر يمنع تطبيق إعدادات
        // ApplicationUser من الـ Configuration class
        // استخدمناه هنا لتجنب تعارض الـ
        // TPH/TPT
        // بين ApplicationUser و subclasses
        //builder.ApplyConfiguration(new ApplicationUserConfiguration());
    }

    }

}
    
