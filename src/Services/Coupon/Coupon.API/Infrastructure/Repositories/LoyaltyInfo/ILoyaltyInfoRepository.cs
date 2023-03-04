using System.Threading.Tasks;

namespace Coupon.API.Infrastructure.Repositories
{
    public interface ILoyaltyInfoRepository
    {
        Task<int?> GetPointsAsync(int byerId);

        Task<int> IncreasePointsBySpentAmountAsync(int buyerId, decimal amount);
        Task DecreasePointsBySpentAmountAsync(int buyerId, decimal amount);
    }
}
