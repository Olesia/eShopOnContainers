using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.API.IntegrationEvents.Events
{
    public record OrderLoyaltyRejectedAddingIntegrationEvent : IntegrationEvent
    {
        public int ByuerId { get; }

        public decimal Amount { get; }

        public OrderLoyaltyRejectedAddingIntegrationEvent(int buyerId, decimal amount)
        {
            ByuerId = buyerId;
            Amount = amount;
        }
    }
}
