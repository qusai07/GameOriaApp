

using GameOria.Domains.Common;
using GameOria.Domains.Entities.Orders;

namespace GameOria.Domains.Entities.Cards
{
    public class CardCode : BaseAuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }

        // Foreign keys
        public Guid CardId { get; set; }
        public Guid? OrderId { get; set; }

        // Navigation properties
        public virtual Card Card { get; set; }
        public virtual Order Order { get; set; }

        // Code metadata
        public DateTime? ExpirationDate { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;

        // Validation and security
        public bool IsValid { get; set; } = true;
        public string ValidationHash { get; set; } = string.Empty;
        public int RedemptionAttempts { get; set; }
        public DateTime? LastAttemptDate { get; set; }

        // Audit
        public string? RedeemedBy { get; set; }
        public string? RedeemedIp { get; set; }
    }
}
