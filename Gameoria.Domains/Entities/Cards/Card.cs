using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Stores;
using Gameoria.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Cards
{
    public class Card : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Money Price { get; set; }
        public bool IsActive { get; set; } = true;

        // Foreign keys
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Store Store { get; set; }
        public virtual CardCategory Category { get; set; }
        public virtual ICollection<CardCode> Codes { get; set; } = new List<CardCode>();

        // Metadata
        public string Publisher { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty; // e.g., "PlayStation", "Xbox", "Steam"
        public string Currency { get; set; } = string.Empty; // e.g., "USD", "EUR"
        public decimal Value { get; set; } // The actual value of the card

        // Inventory tracking
        public int AvailableQuantity { get; set; }
        public int MinimumStockLevel { get; set; }

        // Validation
        public bool RequiresAge18Verification { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
