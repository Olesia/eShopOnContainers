namespace Ordering.API.Application.Commands
{
    public class LoyaltyPointsUsedCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderId { get; }

        [DataMember]
        public int Points { get; }

        public LoyaltyPointsUsedCommand(int orderId, int points)
        {
            OrderId = orderId;
            Points = points;
        }
    }
}
