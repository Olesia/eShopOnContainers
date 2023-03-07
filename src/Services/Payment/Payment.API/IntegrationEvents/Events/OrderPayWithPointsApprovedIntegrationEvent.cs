namespace Payment.API.IntegrationEvents.Events
{
    public record OrderPayWithPointsApprovedIntegrationEvent : IntegrationEvent
    {
        //[JsonProperty]
        public int OrderId { get; }
        public int BuyerId { get; }
        public decimal Total { get; }

        public OrderPayWithPointsApprovedIntegrationEvent(int orderId, int buyerId, decimal total)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            Total = total;
        }
    }
}
