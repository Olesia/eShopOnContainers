using Coupon.API.Infrastructure.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Coupon.API.Infrastructure.Repositories
{
    public class LoyaltyInfoRepository : ILoyaltyInfoRepository
    {
        private readonly LoyaltyInfoContext _loyaltyContext;

        public LoyaltyInfoRepository(LoyaltyInfoContext pointContext)
        {
            _loyaltyContext = pointContext;
        }

        public async Task<int?> GetPointsAvailableAsync(int buyerId)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var loyalty = await _loyaltyContext.LoyaltyInfo.Find(filter).FirstOrDefaultAsync();
            return loyalty?.PointsAvailable;
        }

        public async Task<int?> GetPointsTotalCollectedAsync(int buyerId)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var loyalty = await _loyaltyContext.LoyaltyInfo.Find(filter).FirstOrDefaultAsync();
            return loyalty?.PointsTotalCollected;
        }

        public async Task IncreasePointsAsync(int buyerId, int points)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var update = Builders<LoyaltyInfo>.Update
                        .Inc(loyalty => loyalty.PointsAvailable, points)
                        .Inc(loyalty => loyalty.PointsTotalCollected, points);

            await _loyaltyContext.LoyaltyInfo.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task DecreasePointsAsync(int buyerId, int points)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var update = Builders<LoyaltyInfo>.Update
                        .Inc(loyalty => loyalty.PointsAvailable, -points);

            await _loyaltyContext.LoyaltyInfo.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
        }
    }
}
