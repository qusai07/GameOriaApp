using GameOria.Domains.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.Order)
               .WithMany(o => o.Items)
               .HasForeignKey(i => i.OrderId);

        // Owned type configuration for UnitPrice
        builder.OwnsOne(i => i.UnitPrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("UnitPriceAmount");
            money.Property(m => m.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(3);
        });

        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(255);
        builder.Property(i => i.Platform).HasMaxLength(100);
        builder.Property(i => i.Region).HasMaxLength(100);
        builder.Property(i => i.StoreName).HasMaxLength(255);
    }
}
