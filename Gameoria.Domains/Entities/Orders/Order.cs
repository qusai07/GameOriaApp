using GameOria.Domains.Common;
using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Enums;
using GameOria.Domains.ValueObjects;

namespace GameOria.Domains.Entities.Orders
{
    public class Order : BaseAuditableEntity
    {
        // Basic info
        public string OrderNumber { get; private set; } = string.Empty;
        public Guid UserId { get; private set; } 
        public OrderStatus Status { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public Money TotalAmount { get; private set; } = Money.Zero();

        // Navigation
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public virtual ICollection<OrderCode> Codes { get; set; } = new List<OrderCode>();

        // Payment
        public string? PaymentMethod { get; private set; }
        public string? PaymentTransactionId { get; private set; }
        public DateTime? PaymentDate { get; private set; }

        // Dates
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public DateTime? CompletedDate { get; private set; }
        public DateTime? CancelledDate { get; private set; }

        // Additional info
        public string? Notes { get; set; }
        public string? CancellationReason { get; private set; }
        public string? CustomerIp { get; private set; }

        private Order() { } // EF Core constructor

        public static Order Create(Guid userId, string? customerIp = null)
        {
            return new Order
            {
                UserId = userId,
                CustomerIp = customerIp,
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}".ToUpper(),
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                TotalAmount = Money.Zero()
            };
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = new Money(
                Items.Sum(i => i.SubTotal.Amount),
                Items.FirstOrDefault()?.SubTotal.Currency ?? "USD"
            );
        }

        public void SetPaymentMethod(string method)
        {
            PaymentMethod = method ?? throw new ArgumentNullException(nameof(method));
        }

        public void UpdatePaymentStatus(PaymentStatus status, string? transactionId = null)
        {
            PaymentStatus = status;
            if (transactionId != null)
                PaymentTransactionId = transactionId;
            if (status == PaymentStatus.Completed)
                PaymentDate = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus, string? reason = null)
        {
            Status = newStatus;
            switch (newStatus)
            {
                case OrderStatus.Completed:
                    CompletedDate = DateTime.UtcNow;
                    break;
                case OrderStatus.Cancelled:
                    CancelledDate = DateTime.UtcNow;
                    CancellationReason = reason;
                    break;
            }
        }
    }
}
