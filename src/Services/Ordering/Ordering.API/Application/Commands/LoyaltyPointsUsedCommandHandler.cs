namespace Ordering.API.Application.Commands
{
    public class LoyaltyPointsUsedCommandHandler : IRequestHandler<LoyaltyPointsUsedCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public LoyaltyPointsUsedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(LoyaltyPointsUsedCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(command.OrderId);

            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetPointsUsed(command.Points);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
