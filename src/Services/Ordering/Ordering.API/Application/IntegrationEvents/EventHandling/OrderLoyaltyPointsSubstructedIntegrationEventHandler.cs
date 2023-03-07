using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderLoyaltyPointsSubstructedIntegrationEventHandler : IIntegrationEventHandler<OrderLoyaltyPointsSubstructedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        public OrderLoyaltyPointsSubstructedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderLoyaltyPointsSubstructedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                Log.Information("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                var command = new LoyaltyPointsUsedCommand(@event.OrderId, @event.PointsSubstructed);

                Log.Information("----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.OrderId),
                    command.OrderId,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
