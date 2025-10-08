
using GameOria.Domains.Common;
using GameOria.Domains.Entities.Stores;
using GameOria.Domains.ValueObjects;
namespace GameOria.Domains.Entities.Cards
{
    public class Card : BaseAuditableEntity
    {
        public Card() { }

        public Card(string title, string description, Money price, Guid storeId)
        {
            Title = title;
            Description = description;
            Price = price;
            StoreId = storeId;
            IsActive = true;
        }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Money Price { get; set; } = Money.Zero();
        public bool IsActive { get; set; } = true;
        public bool IsDraft { get; set; } = false;

        // Foreign keys
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Store Store { get; set; }
        public virtual CardCategory? Category { get; set; }
        public virtual ICollection<CardCode> Codes { get; set; } = new List<CardCode>();

        // Optional properties
        public string Region { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;

        // Inventory
        public int AvailableQuantity { get; set; }
        public int MinimumStockLevel { get; set; }

        // Metrics
        public int TotalSales { get; set; }
        public decimal AverageRating { get; set; }

        // Methods
        public bool HasSufficientStock(int requestedQuantity) => AvailableQuantity >= requestedQuantity;

        public void UpdateStock(int quantity)
        {
            AvailableQuantity += quantity;
            if (AvailableQuantity < 0) AvailableQuantity = 0;
        }
    }

}
