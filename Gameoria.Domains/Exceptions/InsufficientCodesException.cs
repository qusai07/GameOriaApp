using GameOria.Domains.Enums;


namespace GameOria.Domains.Exceptions
{
    public class InsufficientCodesException : DomainException
    {
        public Guid ProductId { get; }
        public ProductType ProductType { get; }
        public int RequestedQuantity { get; }
        public int AvailableQuantity { get; }

        public InsufficientCodesException(
            Guid productId,
            ProductType productType,
            int requestedQuantity,
            int availableQuantity)
            : base(
                $"Insufficient codes available for product {productId}. " +
                $"Requested: {requestedQuantity}, Available: {availableQuantity}",
                "INSUFFICIENT_CODES")
        {
            ProductId = productId;
            ProductType = productType;
            RequestedQuantity = requestedQuantity;
            AvailableQuantity = availableQuantity;
        }

        public override IDictionary<string, object[]> GetErrors()
        {
            return new Dictionary<string, object[]>
            {
                { "ProductId", new object[] { ProductId } },
                { "ProductType", new object[] { ProductType } },
                { "RequestedQuantity", new object[] { RequestedQuantity } },
                { "AvailableQuantity", new object[] { AvailableQuantity } },
                { "Message", new object[] { Message } }
            };
        }

        // Helper method to check if quantity is available
        public static bool HasSufficientCodes(int available, int requested)
        {
            return available >= requested;
        }

        // Helper method to get shortage amount
        public int GetShortageAmount()
        {
            return RequestedQuantity - AvailableQuantity;
        }

        // Helper method to get availability percentage
        public decimal GetAvailabilityPercentage()
        {
            if (RequestedQuantity == 0) return 0;
            return (decimal)AvailableQuantity / RequestedQuantity * 100;
        }
    }
}
