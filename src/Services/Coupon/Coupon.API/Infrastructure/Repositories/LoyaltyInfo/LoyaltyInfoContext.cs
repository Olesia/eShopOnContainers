namespace Coupon.API.Infrastructure.Repositories
{
    using Coupon.API.Infrastructure.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class LoyaltyInfoContext
    {
        private readonly IMongoDatabase _database = null;

        public LoyaltyInfoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client is null)
            {
                throw new MongoConfigurationException("Cannot connect to the database. The connection string is not valid or the database is not accessible");
            }

            _database = client.GetDatabase(settings.Value.CouponMongoDatabase);
        }

        public IMongoCollection<LoyaltyInfo> LoyaltyInfo => _database.GetCollection<LoyaltyInfo>("PointsCollection");
    }
}
