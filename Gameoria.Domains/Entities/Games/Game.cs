using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Orders;
using Gameoria.Domains.Entities.Stores;
using Gameoria.Domains.Events.Games;
using Gameoria.Domains.ValueObjects;


namespace Gameoria.Domains.Entities.Games
{
    public class Game : BaseAuditableEntity
    {
        public Game(string title, string description, Money price, string coverImageUrl,
        DateTime releaseDate, Guid storeId)
        {
            Title = title;
            Description = description;
            Price = price;
            ReleaseDate = releaseDate;
            StoreId = storeId;
            IsActive = true;
            IsDraft = true;
        }

        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Money Price { get; set; } = Money.Zero();
        public bool IsActive { get; set; } = true;
        public bool IsPublished { get; set; }
        public bool IsDraft { get; set; } = true;
        public bool IsAvailable { get; set; }
        public bool IsPreOrder { get; set; }
        public bool HasInAppPurchases { get; set; }
        public string Region { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;

        // Foreign keys
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Store? Store { get; set; }
        public virtual GameCategory? Category { get; set; }
        public virtual ICollection<GameCode> Codes { get; set; } = new List<GameCode>();
        public virtual ICollection<GameImage> Images { get; set; } = new List<GameImage>();
        public virtual ICollection<GameReview> Reviews { get; set; } = new List<GameReview>();
        public virtual ICollection<GameCategory> Categories { get; set; } = new List<GameCategory>();

        // Game specific properties
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string MinimumSystemRequirements { get; set; } = string.Empty;
        public string RecommendedSystemRequirements { get; set; } = string.Empty;

        // Rating and Age restrictions
        public string AgeRating { get; set; } = string.Empty;
        public bool RequiresAge18Verification { get; set; }

        // Inventory tracking
        public int AvailableQuantity { get; set; }
        public int MinimumStockLevel { get; set; }

        // Metrics
        public int TotalSales { get; set; }
        public decimal AverageRating { get; set; }

        // Methods
        public bool HasSufficientStock(int requestedQuantity)
        {
            return AvailableQuantity >= requestedQuantity;
        }

        public void UpdateStock(int quantity)
        {
            AvailableQuantity += quantity;
            if (AvailableQuantity < 0)
                AvailableQuantity = 0;
        }

        public void UpdateAverageRating()
        {
            if (Reviews != null && Reviews.Any())
            {
                AverageRating = Reviews.Average(r => r.Rating);
            }
            else
            {
                AverageRating = 0;
            }
        }
    }
}
