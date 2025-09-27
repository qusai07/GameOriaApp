using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;
using GameOria.Domains.Entities.Games;


namespace GameOria.Domains.Entities.Orders
{
    public class OrderCode : BaseAuditableEntity
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public string ProductType { get; set; } // "Game" or "Card"
        public string Code { get; set; } = string.Empty;

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual GameCode GameCode { get; set; }
        public virtual CardCode CardCode { get; set; }

        // Code status
        public bool IsRevealed { get; set; }
        public DateTime? RevealedAt { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedAt { get; set; }

        // Security
        public string CodeHash { get; set; } = string.Empty;
        public string RevealedToIp { get; set; } = string.Empty;

        // Validation
        public DateTime? ExpirationDate { get; set; }
        public bool IsValid { get; set; } = true;
        public string? InvalidationReason { get; set; }

        // Delivery
        public bool IsEmailSent { get; set; }
        public DateTime? EmailSentAt { get; set; }
        public string? EmailSentTo { get; set; }

        // Support
        public bool HasSupportTicket { get; set; }
        public string? SupportTicketId { get; set; }
    }
}
