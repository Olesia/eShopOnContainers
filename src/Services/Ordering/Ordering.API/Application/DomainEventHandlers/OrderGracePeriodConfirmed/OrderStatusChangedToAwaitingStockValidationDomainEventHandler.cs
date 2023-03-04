using Ordering.Domain.Events;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.DomainEventHandlers.OrderGracePeriodConfirmed;
    
public class OrderStatusChangedToAwaitingStockValidationDomainEventHandler
                : INotificationHandler<OrderStatusChangedToAwaitingStockValidationDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILoggerFactory _logger;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public OrderStatusChangedToAwaitingStockValidationDomainEventHandler(
        IOrderRepository orderRepository, ILoggerFactory logger,
        IBuyerRepository buyerRepository,
        IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _buyerRepository = buyerRepository;
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    public async Task Handle(OrderStatusChangedToAwaitingStockValidationDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.CreateLogger<OrderStatusChangedToAwaitingStockValidationDomainEvent>()
            .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                domainEvent.OrderId, nameof(OrderStatus.AwaitingStockValidation), OrderStatus.AwaitingStockValidation.Id);

        var order = await _orderRepository.GetAsync(domainEvent.OrderId);

        var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId.Value.ToString());

        var orderStockList = domainEvent.OrderItems
            .Select(orderItem => new OrderStockItem(orderItem.ProductId, orderItem.GetUnits()));

        var orderStatusChangedToAwaitingStockValidationIntegrationEvent = new OrderStatusChangedToAwaitingStockValidationIntegrationEvent(
            order.Id, order.OrderStatus.Name, buyer.Name, orderStockList);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToAwaitingStockValidationIntegrationEvent);
    }
}
