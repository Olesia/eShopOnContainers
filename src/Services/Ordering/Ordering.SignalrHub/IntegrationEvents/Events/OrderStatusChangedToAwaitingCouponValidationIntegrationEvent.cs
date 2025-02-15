﻿using Newtonsoft.Json;

namespace Ordering.SignalrHub.IntegrationEvents.Events
{
    public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent : IntegrationEvent
    {
        [JsonProperty]
        public int OrderId { get; private set; }

        [JsonProperty]
        public string OrderStatus { get; private set; }

        [JsonProperty]
        public string BuyerName { get; private set; }

        [JsonProperty]
        public string Code { get; private set; }

        public OrderStatusChangedToAwaitingCouponValidationIntegrationEvent(int orderId, string orderStatus, string buyerName, string code)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
            Code = code;
        }
    }
}
