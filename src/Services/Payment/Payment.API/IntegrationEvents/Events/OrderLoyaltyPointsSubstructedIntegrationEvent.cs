namespace Payment.API.IntegrationEvents.Events
{
    public record OrderLoyaltyPointsSubstructedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public int BuyerId { get; }
        public decimal Total { get; }
        public int PointsSubstructed { get; }

        public OrderLoyaltyPointsSubstructedIntegrationEvent(int orderId, int buyerId, decimal total, int pointsSubstructed)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            Total = total;
            PointsSubstructed = pointsSubstructed;
        }
    }
}
