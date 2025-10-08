
using GameOria.Domains.Common;
using GameOria.Domains.Entities.Stores;
using GameOria.Domains.ValueObjects;


namespace GameOria.Domains.Entities.Games
{
    public class Game : BaseAuditableEntity
    {
        public Game() { }

        public Game(string title, string description, Money price, string coverImageUrl,
                    DateTime releaseDate, Guid storeId)
        {
            Title = title;
            Description = description;
            Price = price;
            CoverImageUrl = coverImageUrl;
            ReleaseDate = releaseDate;
            StoreId = storeId;
            IsActive = true;
            IsDraft = false;
        }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Money Price { get; set; } = Money.Zero();
        public bool IsActive { get; set; } = true;
        public bool IsPublished { get; set; }
        public bool IsDraft { get; set; } = false;
        public bool IsPreOrder { get; set; }
        public bool HasInAppPurchases { get; set; }
        public string Region { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;

        // Foreign keys
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Store Store { get; set; }
        public virtual GameCategory? Category { get; set; }
        public virtual ICollection<GameCode> Codes { get; set; } = new List<GameCode>();
        public virtual ICollection<GameImage> Images { get; set; } = new List<GameImage>();
        public virtual ICollection<GameReview> Reviews { get; set; } = new List<GameReview>();

        // Game metadata
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        // Optional system requirements
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
        public bool HasSufficientStock(int requestedQuantity) => AvailableQuantity >= requestedQuantity;

        public void UpdateStock(int quantity)
        {
            AvailableQuantity += quantity;
            if (AvailableQuantity < 0) AvailableQuantity = 0;
        }

        public void UpdateAverageRating()
        {
            AverageRating = Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        }
    }

}
