using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameOria.Domains.Entities.Cards;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .HasMaxLength(1000);

        builder.Property(c => c.Region)
               .HasMaxLength(100);

        builder.Property(c => c.Platform)
               .HasMaxLength(100);

        builder.OwnsOne(c => c.Price, money =>
        {
            money.Property(m => m.Amount).HasColumnName("PriceAmount");
            money.Property(m => m.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
        });

        // Relations
        builder.HasMany(c => c.Codes)
               .WithOne(c => c.Card)
               .HasForeignKey(c => c.CardId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Category)
               .WithMany(cat => cat.Cards)
               .HasForeignKey(c => c.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Store)
               .WithMany(s => s.Cards)
               .HasForeignKey(c => c.StoreId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.IsActive).HasDefaultValue(true);
    }
}
