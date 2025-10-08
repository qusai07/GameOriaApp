using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;
using GameOria.Domains.Entities.Games;
using GameOria.Domains.Entities.Identity;


namespace GameOria.Domains.Entities.Stores
{
    public class Store : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; }

        // Contact
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        // Metrics
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalSales { get; set; }
        public DateTime? LastSaleDate { get; set; } // nullable لأن ممكن لا يوجد بيع

        // FK to Owner
        public Guid StoreOwnerId { get; set; } // ربط مباشر بـ StoreOwner
        public virtual StoreOwner StoreOwner { get; set; }

        // Navigation
        public virtual ICollection<StoreReview> Reviews { get; set; } = new List<StoreReview>();
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

        // Settings
        public bool AutoApproveReviews { get; set; }
        public bool AllowDigitalProducts { get; set; } = true;

        // JSON arrays
        public string[] AcceptedPaymentMethods { get; set; } = Array.Empty<string>();
        public string[] SupportedCurrencies { get; set; } = Array.Empty<string>();

        // Commission
        public decimal CommissionRate { get; set; }
        public CommissionType CommissionType { get; set; } = CommissionType.Percentage; // enum
    }

    // enum
    public enum CommissionType
    {
        Percentage,
        Fixed
    }

}
