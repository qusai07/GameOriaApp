
using GameOria.Domains.Common;
using GameOria.Domains.Enums;

namespace GameOria.Domains.Events.Orders
{
    public class OrderStatusChangedEvent : BaseEntity
    {
        public Guid OrderId { get; }
        public OrderStatus OldStatus { get; }
        public OrderStatus NewStatus { get; }
        public string ChangedBy { get; }
        public DateTime ChangedAt { get; }
        public string? Reason { get; }

        public OrderStatusChangedEvent(
            Guid orderId,
            OrderStatus oldStatus,
            OrderStatus newStatus,
            string changedBy,
            string? reason = null)
        {
            OrderId = orderId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedBy = changedBy;
            ChangedAt = DateTime.UtcNow;
            Reason = reason;
        }

        // Helper method to check if status change requires notification
        public bool RequiresNotification()
        {
            return NewStatus switch
            {
                OrderStatus.PaymentReceived => true,
                OrderStatus.CodeAssigned => true,
                OrderStatus.Completed => true,
                OrderStatus.Cancelled => true,
                OrderStatus.Failed => true,
                OrderStatus.Refunded => true,
                _ => false
            };
        }

        // Helper method to get notification message
        public string GetNotificationMessage()
        {
            return NewStatus switch
            {
                OrderStatus.PaymentReceived => $"Payment received for order {OrderId}",
                OrderStatus.CodeAssigned => $"Codes have been assigned to order {OrderId}",
                OrderStatus.Completed => $"Order {OrderId} has been completed",
                OrderStatus.Cancelled => $"Order {OrderId} has been cancelled. Reason: {Reason}",
                OrderStatus.Failed => $"Order {OrderId} has failed. Reason: {Reason}",
                OrderStatus.Refunded => $"Order {OrderId} has been refunded",
                _ => $"Order {OrderId} status changed from {OldStatus} to {NewStatus}"
            };
        }

        // Helper method to check if status change requires refund processing
        public bool RequiresRefund()
        {
            return NewStatus == OrderStatus.Cancelled ||
                   NewStatus == OrderStatus.Refunded;
        }

        // Helper method to get audit log entry
        public Dictionary<string, object> GetAuditLogEntry()
        {
            return new Dictionary<string, object>
            {
                { "OrderId", OrderId },
                { "OldStatus", OldStatus },
                { "NewStatus", NewStatus },
                { "ChangedBy", ChangedBy },
                { "ChangedAt", ChangedAt },
                { "Reason", Reason ?? "Not specified" }
            };
        }
    }
}
