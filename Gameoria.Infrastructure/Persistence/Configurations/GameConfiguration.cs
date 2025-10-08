using GameOria.Domains.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GameOria.Infrastructure.Persistence.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title).IsRequired().HasMaxLength(200);

            builder.HasOne(g => g.Category)
                   .WithMany(c => c.Games)
                   .HasForeignKey(g => g.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Codes)
                   .WithOne(c => c.Game)
                   .HasForeignKey(c => c.GameId);

            builder.HasMany(g => g.Images)
                   .WithOne(i => i.Game)
                   .HasForeignKey(i => i.GameId);

            builder.HasMany(g => g.Reviews)
                   .WithOne(r => r.Game)
                   .HasForeignKey(r => r.GameId);


            builder.OwnsOne(g => g.Price, money =>
            {
                money.Property(m => m.Amount).HasColumnName("PriceAmount");
                money.Property(m => m.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
            });

        }
    }
}
