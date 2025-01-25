using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Orders;
using Gameoria.Domains.ValueObjects;


namespace Gameoria.Domains.Events.Orders
{
    public class OrderCreatedEvent : BaseEntity
    {
        public Order Order { get; }
        public int CustomerId { get; } // Changed to int to match User's Id type
        public DateTime CreatedAt { get; }
        public Money TotalAmount { get; }

        public OrderCreatedEvent(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            CustomerId = order.UserId;
            CreatedAt = DateTime.UtcNow;
            TotalAmount = order.TotalAmount;
        }

        // Helper method to get order summary
        public Dictionary<string, object> GetOrderSummary()
        {
            return new Dictionary<string, object>
            {
                { "OrderId", Order.Id },
                { "OrderNumber", Order.OrderNumber },
                { "CustomerId", CustomerId },
                { "CreatedAt", CreatedAt },
                { "TotalAmount", TotalAmount.ToString() },
                { "ItemCount", Order.Items.Count },
                { "PaymentMethod", Order.PaymentMethod ?? "Not Set" },
                { "PaymentStatus", Order.PaymentStatus.ToString() },
                { "OrderStatus", Order.Status.ToString() }
            };
        }

        // Helper method to get items summary
        public IEnumerable<Dictionary<string, object>> GetOrderItemsSummary()
        {
            return Order.Items.Select(item => new Dictionary<string, object>
            {
                { "ProductId", item.ProductId },
                { "ProductType", item.ProductType.ToString() },
                { "Quantity", item.Quantity },
                { "UnitPrice", item.UnitPrice.ToString() },
                { "Subtotal", item.SubTotal.ToString() }
            });
        }

        // Helper method to check if order requires special handling
        public bool RequiresSpecialHandling()
        {
            return TotalAmount.Amount > 1000 ||
                    Order.Items.Count > 5 ||
                    Order.Items.Any(i => i.IsPreOrder());
        }

        // Helper method to get notification recipients
        public IEnumerable<string> GetNotificationRecipients()
        {
            var recipients = new List<string> { CustomerId.ToString() };

            // Add store owners for each item in the order
            var storeIds = Order.Items
                .Select(item => item.StoreId)
                .Distinct();

            recipients.AddRange(storeIds.Select(id => id.ToString()));

            return recipients;
        }
    }
}
