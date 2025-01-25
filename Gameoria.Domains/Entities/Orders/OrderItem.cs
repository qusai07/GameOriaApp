using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Cards;
using Gameoria.Domains.Entities.Games;
using Gameoria.Domains.Enums;
using Gameoria.Domains.Events.Orders;
using Gameoria.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Orders
{
    public class OrderItem : BaseAuditableEntity
    {
        public Guid OrderId { get; set; }
        public ProductType ProductType { get; set; } // "Game" or "Card"
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public Money UnitPrice { get; set; }
        public Money SubTotal { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Game? Game { get; set; }
        public virtual Card Card { get; set; }

        // Product details at time of purchase
        public string ProductSku { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        // Status tracking
        public bool IsCodeAssigned { get; set; }
        public DateTime? CodeAssignedAt { get; set; }

        // Store information
        public Guid StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public bool IsPreOrder()
        {
            return ProductType == ProductType.Game && Game?.IsPreOrder == true;
        }
    }
}
