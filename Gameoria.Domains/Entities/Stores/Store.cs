using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Cards;
using Gameoria.Domains.Entities.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Stores
{
    public class Store : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; }

        // Store Contact Information
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        // Social Media Links
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }

        // Business Information
        public string BusinessRegistrationNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty;

        // Store Metrics
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalSales { get; set; }
        public DateTime LastSaleDate { get; set; }

        // Navigation Properties
        public string OwnerId { get; set; } // ApplicationUser Id
        public virtual User.User Owner { get; set; }
        public virtual StoreOwner StoreOwner { get; set; }
        public virtual ICollection<StoreReview> Reviews { get; set; } = new List<StoreReview>();
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

        // Store Settings
        public bool AutoApproveReviews { get; set; }
        public bool AllowDigitalProducts { get; set; } = true;
        public string[] AcceptedPaymentMethods { get; set; } = Array.Empty<string>();
        public string[] SupportedCurrencies { get; set; } = Array.Empty<string>();

        // Commission Settings
        public decimal CommissionRate { get; set; }
        public string CommissionType { get; set; } = "Percentage"; // Percentage or Fixed
    }
}
