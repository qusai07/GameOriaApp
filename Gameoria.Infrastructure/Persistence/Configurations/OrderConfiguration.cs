using GameOria.Domains.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.OrderNumber).IsUnique();

            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId);

            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(i => i.OrderId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Codes)
                   .WithOne(c => c.Order)
                   .HasForeignKey(c => c.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(o => o.TotalAmount, money =>
            {
                money.Property(m => m.Amount).HasColumnName("TotalAmount");
                money.Property(m => m.Currency).HasColumnName("TotalCurrency").HasMaxLength(3);
            });
        }
    }
}
