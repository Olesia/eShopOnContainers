using Newtonsoft.Json;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderCouponConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public int Discount { get; }
        public OrderCouponConfirmedIntegrationEvent(int orderId, int discount)
        {
            OrderId = orderId;
            Discount = discount;
        }
    }
}
