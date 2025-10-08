
using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;
using GameOria.Domains.Entities.Games;
using GameOria.Domains.Enums;
using GameOria.Domains.ValueObjects;


namespace GameOria.Domains.Entities.Orders
{
    public class OrderItem : BaseAuditableEntity
    {
        // Foreign keys
        public Guid OrderId { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ProductId { get; set; }

        // Basic info
        public string ProductName { get; set; } = string.Empty;
        public Money UnitPrice { get; set; } = Money.Zero();
        public int Quantity { get; set; }

        // Navigation
        public virtual Order Order { get; set; } = null!;
        public virtual Game? Game { get; set; }
        public virtual Card? Card { get; set; }

        // Calculated property
        public Money SubTotal => UnitPrice.Multiply(Quantity);

        // Metadata
        public string ProductSku { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        // Code tracking
        public bool IsCodeAssigned { get; set; }
        public DateTime? CodeAssignedAt { get; set; }

        // Store info
        public Guid StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;

        public bool IsPreOrder()
        {
            return ProductType == ProductType.Game && Game?.IsPreOrder == true;
        }
    }
}
