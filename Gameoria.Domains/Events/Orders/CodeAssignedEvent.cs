using Gameoria.Domains.Common;
using Gameoria.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Events.Orders
{
    public class CodeAssignedEvent :BaseEntity
    {
        public Guid OrderId { get; }
        public Guid OrderItemId { get; }
        public string Code { get; }
        public ProductType ProductType { get; }
        public string AssignedBy { get; }
        public DateTime AssignedAt { get; }
        public DateTime ExpiresAt { get; }

        public CodeAssignedEvent(
            Guid orderId,
            Guid orderItemId,
            string code,
            ProductType productType,
            string assignedBy,
            DateTime? expiresAt = null)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            Code = code;
            ProductType = productType;
            AssignedBy = assignedBy;
            AssignedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt ?? DateTime.UtcNow.AddYears(1);
        }

        // Helper method to get code details for notification
        public Dictionary<string, object> GetCodeDetails()
        {
            return new Dictionary<string, object>
            {
                { "OrderId", OrderId },
                { "OrderItemId", OrderItemId },
                { "ProductType", ProductType },
                { "AssignedAt", AssignedAt },
                { "ExpiresAt", ExpiresAt },
                { "IsExpired", DateTime.UtcNow > ExpiresAt }
            };
        }

        // Helper method to check if code is expired
        public bool IsCodeExpired()
        {
            return DateTime.UtcNow > ExpiresAt;
        }

        // Helper method to get time until expiration
        public TimeSpan GetTimeUntilExpiration()
        {
            return ExpiresAt - DateTime.UtcNow;
        }

        // Helper method to get notification message
        public string GetNotificationMessage()
        {
            return $"Code for {ProductType} has been assigned to your order {OrderId}. " +
                   $"The code will expire on {ExpiresAt:d}";
        }

        // Helper method to get audit log entry
        public Dictionary<string, object> GetAuditLogEntry()
        {
            return new Dictionary<string, object>
            {
                { "OrderId", OrderId },
                { "OrderItemId", OrderItemId },
                { "ProductType", ProductType },
                { "AssignedBy", AssignedBy },
                { "AssignedAt", AssignedAt },
                { "ExpiresAt", ExpiresAt },
                { "CodeHash", Code.GetHashCode() } // For security, we don't log the actual code
            };
        }
    }
}
