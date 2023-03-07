namespace Coupon.API.Dtos.LoyaltyInfo
{
    using Coupon.API.Infrastructure.Models;

    public class LoyaltyInfoMapper : IMapper<LoyaltyInfoDto, LoyaltyInfo>
    {
        public LoyaltyInfoDto Translate(LoyaltyInfo entity)
        {
            return new LoyaltyInfoDto
            {
                BuyerId = entity.BuyerId,
                PointsAvailable = entity.PointsAvailable,
                PointsTotalCollected = entity.PointsTotalCollected
            };
        }
    }
}
