using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coupon.API.Infrastructure.Models
{
    public class LoyaltyInfo
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BuyerId { get; set; }

        public int PointsAvailable { get; set; }

        public int PointsTotalCollected { get; set; }

    }
}
