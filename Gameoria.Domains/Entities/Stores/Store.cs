using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;
using GameOria.Domains.Entities.Games;
using GameOria.Domains.Entities.Identity;

namespace GameOria.Domains.Entities.Stores
{
    public class Store : BaseAuditableEntity
    {
        // Basic Store Info
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; }

        // Contact
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ShortcutWebsite { get; set; } = string.Empty;

        // Metrics
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalSales { get; set; }
        public DateTime? LastSaleDate { get; set; }

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


        public Guid UserId { get; set; } // FK to Identity/User

        // Personal
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string OwnerPhone { get; set; } = string.Empty;
        public DateTime OwnerDateOfBirth { get; set; }

        // Verification
        public DateTime? VerificationDate { get; set; }
        public string? VerificationDocument { get; set; }

        // Banking
        public string BankName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankRoutingNumber { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;

        // Agreement
        public bool HasAcceptedTerms { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public string? TermsAcceptanceIp { get; set; }
    }

    public enum CommissionType
    {
        Percentage,
        Fixed
    }
}
