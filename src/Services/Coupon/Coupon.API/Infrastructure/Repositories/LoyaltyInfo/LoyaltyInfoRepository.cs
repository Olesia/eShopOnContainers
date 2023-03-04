using Coupon.API.Infrastructure.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Coupon.API.Infrastructure.Repositories
{
    public class LoyaltyInfoRepository : ILoyaltyInfoRepository
    {
        private readonly LoyaltyInfoContext _loyaltyContext;
        private const decimal loyaltyPercentage = (decimal)0.1;

        public LoyaltyInfoRepository(LoyaltyInfoContext pointContext)
        {
            _loyaltyContext = pointContext;
        }

        public async Task<int?> GetPointsAsync(int buyerId)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var loyalty = await _loyaltyContext.LoyaltyInfo.Find(filter).FirstOrDefaultAsync();
            return loyalty?.Points;
        }

        public async Task<int> IncreasePointsBySpentAmountAsync(int buyerId, decimal amount)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var points = RoundPoints(amount);
            var update = Builders<LoyaltyInfo>.Update
                        .Inc(loyalty => loyalty.Points, points);

            await _loyaltyContext.LoyaltyInfo.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
            return points;
        }

        public async Task DecreasePointsBySpentAmountAsync(int buyerId, decimal amount)
        {
            var filter = Builders<LoyaltyInfo>.Filter.Eq("BuyerId", buyerId);
            var update = Builders<LoyaltyInfo>.Update
                        .Inc(loyalty => loyalty.Points, - RoundPoints(amount));

            await _loyaltyContext.LoyaltyInfo.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
        }

        public int RoundPoints(decimal amount)
        {
            return (int)(amount *loyaltyPercentage);
        }
    }
}
