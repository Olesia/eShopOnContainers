using Coupon.API.Infrastructure.Repositories;
using Coupon.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Serilog;
using Serilog.Context;
using System.Threading.Tasks;

namespace Coupon.API.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSucceededIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>
    {
        private readonly ILoyaltyInfoRepository _loyaltyRepository;
        private readonly IEventBus _eventBus;

        public OrderPaymentSucceededIntegrationEventHandler(ILoyaltyInfoRepository loyaltyRepository, IEventBus eventBus)
        {
            _loyaltyRepository = loyaltyRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(OrderPaymentSucceededIntegrationEvent @event)
        {
            await Task.Delay(3000);

            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Loyalty.API"))
            {
                Log.Information("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Coupon.API", @event);
                var points = await _loyaltyRepository.IncreasePointsBySpentAmountAsync(@event.BuyerId, @event.Total);

                Log.Information("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", @event.Id, "Coupon.API", @event);
                var integrationEvent = new OrderLoyaltyPointsAddedIntegrationEvent(@event.OrderId, points);
                _eventBus.Publish(integrationEvent);
            }
        }
    }
}
