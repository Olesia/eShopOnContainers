using System;

namespace Coupon.API.Helpers
{
    public static class PointCalculator
    {
        
        private const decimal standartTierPercentage = (decimal)0.1;
        private const decimal silverTierPercentage = (decimal)0.2;
        private const decimal goldenTierPercentage = (decimal)0.3;

        private const int standartTierPointsAmount = 1000;
        private const int goldenTierPointsAmount = 3000;

        public static int CalculatePointsToAccumulate(int? userTotalCollectedPoints, decimal amountToPay)
        {
            var loyaltyPercentage = userTotalCollectedPoints switch
            {
                null => standartTierPercentage,
                <= standartTierPointsAmount => standartTierPercentage,
                >= goldenTierPointsAmount => goldenTierPercentage,
                _=> silverTierPercentage,
            };
            return (int)(amountToPay * loyaltyPercentage);
        }

        public static int CalculatePointsToPay(int existedPoints, decimal amount)
        {
            var celAmount = Math.Ceiling(amount);
            return (int)Math.Min(celAmount, existedPoints);
        }
    }
}
