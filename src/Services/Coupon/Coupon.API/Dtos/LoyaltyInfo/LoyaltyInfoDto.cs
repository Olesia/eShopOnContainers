namespace Coupon.API.Dtos.LoyaltyInfo
{
    public class LoyaltyInfoDto
    {
        public string BuyerId { get; set; }
        public int PointsAvailable { get; set; }
        public int PointsTotalCollected { get; set; }
    }
}
