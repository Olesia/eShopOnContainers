namespace Microsoft.eShopOnContainers.Payment.API.IntegrationEvents.Events;

public record OrderPaymentSucceededIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public int BuyerId { get; }

    public decimal Total { get; }

    public OrderPaymentSucceededIntegrationEvent(int orderId, int buyerId, decimal total)
    {
        OrderId = orderId;
        BuyerId = buyerId;
        Total = total;
    }
}
