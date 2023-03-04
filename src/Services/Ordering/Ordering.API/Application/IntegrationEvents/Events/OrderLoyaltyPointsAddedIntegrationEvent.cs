namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderLoyaltyPointsAddedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public int Points { get; }

        public OrderLoyaltyPointsAddedIntegrationEvent(int orderId, int points)
        {
            OrderId = orderId;
            Points = points;
        }
    }
}
