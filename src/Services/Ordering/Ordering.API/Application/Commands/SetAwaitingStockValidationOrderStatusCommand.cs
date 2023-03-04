namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;

public class SetAwaitingStockValidationOrderStatusCommand : IRequest<bool>
{

    [DataMember]
    public int OrderNumber { get; private set; }

    public SetAwaitingStockValidationOrderStatusCommand(int orderNumber)
    {
        OrderNumber = orderNumber;
    }
}
