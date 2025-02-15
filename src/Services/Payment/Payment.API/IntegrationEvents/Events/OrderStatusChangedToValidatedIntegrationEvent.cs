﻿using Newtonsoft.Json;

namespace Microsoft.eShopOnContainers.Payment.API.IntegrationEvents.Events;
    
public record OrderStatusChangedToValidatedIntegrationEvent : IntegrationEvent
{
    //[JsonProperty]
    public int OrderId { get; }
    public string OrderStatus { get; }
    public int BuyerId { get; }
    public string BuyerName { get; }
    public bool IsPayWithPointsApproved { get; }
    public decimal Total { get; }

    public OrderStatusChangedToValidatedIntegrationEvent(int orderId, string orderStatus, int buyerId, string buyerName, bool isPayWithPointsApproved, decimal total)
    {
        OrderId = orderId;
        IsPayWithPointsApproved = isPayWithPointsApproved;
        OrderStatus = orderStatus;
        BuyerId = buyerId;
        BuyerName = buyerName;
        Total = total;
    }
}
