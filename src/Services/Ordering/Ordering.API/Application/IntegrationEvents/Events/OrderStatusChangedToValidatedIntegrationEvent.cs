namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToValidatedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string OrderStatus { get; }
    public int BuyerId { get; }
    public string BuyerName { get; }
    public bool IsPayWithPointsApproved { get; }
    public decimal Total { get; }

    public OrderStatusChangedToValidatedIntegrationEvent(int orderId, string orderStatus, int buyerId, string buyerName, bool isPayWithPointsApproved, decimal total)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
        BuyerId = buyerId;
        IsPayWithPointsApproved = isPayWithPointsApproved;
        Total = total;
    }
}