namespace Ordering.API.Application.Commands
{
    public class LoyaltyPointsEarnedCommand:IRequest<bool>
    {
        [DataMember]
        public int OrderId { get; }

        [DataMember]
        public int Points { get; }

        public LoyaltyPointsEarnedCommand(int orderId, int points)
        {
            OrderId = orderId;
            Points = points;
        }
    }
}
