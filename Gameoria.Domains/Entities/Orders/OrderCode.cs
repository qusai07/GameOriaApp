using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;
using GameOria.Domains.Entities.Games;

namespace GameOria.Domains.Entities.Orders
{
    public class OrderCode : BaseAuditableEntity
    {
        public Guid OrderId { get; set; }       
        public Guid OrderItemId { get; set; }
        public string ProductType { get; set; } = string.Empty; 
        public string Code { get; set; } = string.Empty;

        // Navigation
        public virtual Order Order { get; set; } = null!;     
        public virtual OrderItem OrderItem { get; set; } = null!;
        public virtual GameCode? GameCode { get; set; }
        public virtual CardCode? CardCode { get; set; }

        // Status tracking
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

        public static OrderCode CreateFromGameCode(OrderItem item, GameCode code)
        {
            return new OrderCode
            {
                OrderItem = item,
                OrderItemId = item.Id,
                ProductType = "Game",
                Code = code.Code,
                GameCode = code,
                ExpirationDate = code.ExpirationDate,
                IsValid = code.IsValid
            };
        }

        public static OrderCode CreateFromCardCode(OrderItem item, CardCode code)
        {
            return new OrderCode
            {
                OrderItem = item,
                OrderItemId = item.Id,
                ProductType = "Card",
                Code = code.Code,
                CardCode = code,
                ExpirationDate = code.ExpirationDate,
                IsValid = code.IsValid
            };
        }
    }
}
