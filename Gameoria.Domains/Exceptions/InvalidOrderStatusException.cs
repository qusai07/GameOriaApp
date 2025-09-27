

using GameOria.Domains.Enums;

namespace GameOria.Domains.Exceptions
{
    public class InvalidOrderStatusException : DomainException
    {
        public Guid OrderId { get; }
        public OrderStatus CurrentStatus { get; }
        public OrderStatus RequestedStatus { get; }

        public InvalidOrderStatusException(
            Guid orderId,
            OrderStatus currentStatus,
            OrderStatus requestedStatus)
            : base($"Cannot change order {orderId} status from {currentStatus} to {requestedStatus}", "INVALID_ORDER_STATUS")
        {
            OrderId = orderId;
            CurrentStatus = currentStatus;
            RequestedStatus = requestedStatus;
        }

        public InvalidOrderStatusException(
            Guid orderId,
            OrderStatus currentStatus,
            OrderStatus requestedStatus,
            string message)
            : base(message, "INVALID_ORDER_STATUS")
        {
            OrderId = orderId;
            CurrentStatus = currentStatus;
            RequestedStatus = requestedStatus;
        }

        public override IDictionary<string, object[]> GetErrors()
        {
            return new Dictionary<string, object[]>
            {
                { "OrderId", new object[] { OrderId } },
                { "CurrentStatus", new object[] { CurrentStatus } },
                { "RequestedStatus", new object[] { RequestedStatus } },
                { "Message", new object[] { Message } }
            };
        }

        // Helper method to check if status transition is valid
        public static bool IsValidTransition(OrderStatus current, OrderStatus requested)
        {
            return (current, requested) switch
            {
                (OrderStatus.Created, OrderStatus.Pending) => true,
                (OrderStatus.Pending, OrderStatus.PaymentReceived) => true,
                (OrderStatus.PaymentReceived, OrderStatus.Processing) => true,
                (OrderStatus.Processing, OrderStatus.CodeAssigned) => true,
                (OrderStatus.CodeAssigned, OrderStatus.Completed) => true,
                (_, OrderStatus.Cancelled) => true,  // Can be cancelled from any state
                (_, OrderStatus.Failed) => true,     // Can fail from any state
                (OrderStatus.Failed, OrderStatus.Refunded) => true,
                (OrderStatus.Cancelled, OrderStatus.Refunded) => true,
                _ => false
            };
        }
    }
}
