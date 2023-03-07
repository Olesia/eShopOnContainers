using System.Threading.Tasks;

namespace Coupon.API.Infrastructure.Repositories
{
    public interface ILoyaltyInfoRepository
    {
        Task<int?> GetPointsAvailableAsync(int byerId);
        Task<int?> GetPointsTotalCollectedAsync(int byerId);

        Task IncreasePointsAsync(int buyerId, int points);
        Task DecreasePointsAsync(int buyerId, int points);
    }
}
