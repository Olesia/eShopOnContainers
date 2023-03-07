using Coupon.API.Helpers;
using Coupon.API.Infrastructure.Repositories;
using Coupon.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Serilog;
using Serilog.Context;
using System.Threading.Tasks;

namespace Coupon.API.IntegrationEvents.EventHandlers
{
    public class OrderPayWithPointsApprovedIntegrationEventHandler :
        IIntegrationEventHandler<OrderPayWithPointsApprovedIntegrationEvent>
    {
        private readonly ILoyaltyInfoRepository _loyaltyRepository;
        private readonly IEventBus _eventBus;

        public OrderPayWithPointsApprovedIntegrationEventHandler(ILoyaltyInfoRepository loyaltyRepository, IEventBus eventBus)
        {
            _loyaltyRepository = loyaltyRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(OrderPayWithPointsApprovedIntegrationEvent @event)
        {
            await Task.Delay(3000);

            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Loyalty.API"))
            {
                var pointsAvailable = await _loyaltyRepository.GetPointsAvailableAsync(@event.BuyerId);
                Log.Information("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Coupon.API", @event);
                var points = PointCalculator.CalculatePointsToPay(pointsAvailable ?? 0, @event.Total);
                
                await _loyaltyRepository.DecreasePointsAsync(@event.BuyerId, points);

                Log.Information("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", @event.Id, "Coupon.API", @event);
                var integrationEvent = new OrderLoyaltyPointsSubstructedIntegrationEvent(@event.OrderId, @event.BuyerId, @event.Total, points);
                _eventBus.Publish(integrationEvent);
            }
        }
    }
}
