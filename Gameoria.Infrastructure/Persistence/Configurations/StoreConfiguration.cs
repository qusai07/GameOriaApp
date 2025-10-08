using GameOria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace GameOria.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(s => s.Name).IsUnique();

            builder.HasOne(s => s.StoreOwner)
                   .WithOne(so => so.Store)
                   .HasForeignKey<StoreOwner>(so => so.StoreId);

            builder.HasMany(s => s.Games)
                   .WithOne(g => g.Store)
                   .HasForeignKey(g => g.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Cards)
                   .WithOne(c => c.Store)
                   .HasForeignKey(c => c.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Reviews)
                   .WithOne(r => r.Store)
                   .HasForeignKey(r => r.StoreId);
        }

    }
}
