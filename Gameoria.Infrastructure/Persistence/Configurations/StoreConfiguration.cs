using GameOria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOria.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            // Table name
            builder.ToTable("Stores");

            // Properties
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.Description)
                   .HasMaxLength(2000);

            builder.Property(s => s.Email)
                   .HasMaxLength(200);

            builder.Property(s => s.Phone)
                   .HasMaxLength(50);

            builder.Property(s => s.Website)
                   .HasMaxLength(500);

            //builder.HasOne(s => s.ApplicationUser)
            //        .WithOne(u => u.Store) 
            //        .HasForeignKey<Store>(s => s.OwnerId)
            //        .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(s => s.StoreOwner)
                   .WithOne(o => o.Store)
                   .HasForeignKey<StoreOwner>(o => o.StoreId);

            //builder.HasMany(s => s.Reviews)
            //       .WithOne(r => r.Store)
            //       .HasForeignKey(r => r.StoreId);

            //builder.HasMany(s => s.Games)
            //       .WithOne(g => g.Store)
            //       .HasForeignKey(g => g.StoreId);

            //builder.HasMany(s => s.Cards)
            //       .WithOne(c => c.Store)
            //       .HasForeignKey(c => c.StoreId);

            // Value conversions (lists as string)
            //builder.Property(s => s.AcceptedPaymentMethods)
            //       .HasConversion(
            //           v => string.Join(',', v),          
            //           v => v.Split(',', StringSplitOptions.RemoveEmptyEntries) 
            //       );

            //builder.Property(s => s.SupportedCurrencies)
            //       .HasConversion(
            //           v => string.Join(',', v),
            //           v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            //       );

        }
    }
}
