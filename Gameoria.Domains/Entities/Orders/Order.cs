using Gameoria.Domains.Common;
using Gameoria.Domains.Enums;
using Gameoria.Domains.ValueObjects;

namespace Gameoria.Domains.Entities.Orders
{
    public class Order : BaseAuditableEntity
    {
        public string OrderNumber { get; private set; }
        public int UserId { get; set; } // Changed to int to match User's Id type
        public OrderStatus Status { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public Money TotalAmount { get; private set; }

        // Navigation properties
        public virtual User.User User { get; set; } = null!;
        public virtual ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public virtual ICollection<OrderCode> Codes { get; private set; } = new List<OrderCode>();

        // Payment information
        public string? PaymentMethod { get; private set; }
        public string? PaymentTransactionId { get; private set; }
        public DateTime? PaymentDate { get; private set; }

        // Dates
        public DateTime OrderDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public DateTime? CancelledDate { get; private set; }

        // Additional information
        public string? Notes { get; set; }
        public string? CancellationReason { get; private set; }
        public string? CustomerIp { get; set; }

        // Constructor
        private Order() { } // For EF Core

        public Order(int userId, Money totalAmount, string? customerIp = null)
        {
            UserId = userId;
            TotalAmount = totalAmount;
            CustomerIp = customerIp;
            OrderNumber = GenerateOrderNumber();
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
        }

        // Methods
        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8]}".ToUpper();
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

        public void SetPaymentMethod(string method)
        {
            PaymentMethod = method ?? throw new ArgumentNullException(nameof(method));
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            RecalculateTotalAmount();
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = new Money(Items.Sum(i => i.SubTotal.Amount));
        }


    }
}
