namespace Microsoft.eShopOnContainers.Payment.API;

public class PaymentSettings
{
    public bool PaymentSucceeded { get; set; }
    public string EventBusConnection { get; set; }
    public decimal? MaxOrderTotal { get; set; }
}

