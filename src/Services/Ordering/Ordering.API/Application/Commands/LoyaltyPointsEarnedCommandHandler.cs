namespace Ordering.API.Application.Commands
{
    public class LoyaltyPointsEarnedCommandHandler : IRequestHandler<LoyaltyPointsEarnedCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public LoyaltyPointsEarnedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(LoyaltyPointsEarnedCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(command.OrderId);

            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetPointsEarned(command.Points);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
