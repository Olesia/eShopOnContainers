﻿using Coupon.API.Helpers;
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
                var totalCollectedPoints = await _loyaltyRepository.GetPointsTotalCollectedAsync(@event.BuyerId);
                
                var points = PointCalculator.CalculatePointsToAccumulate(totalCollectedPoints, @event.Total);
                await _loyaltyRepository.IncreasePointsAsync(@event.BuyerId, points);

                Log.Information("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", @event.Id, "Coupon.API", @event);
                var integrationEvent = new OrderLoyaltyPointsAddedIntegrationEvent(@event.OrderId, points);
                _eventBus.Publish(integrationEvent);
            }
        }
    }
}
