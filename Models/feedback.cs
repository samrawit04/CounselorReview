using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace counselorReview.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CounselorId { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ClientId { get; set; }

        [BsonRequired]
        public required string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}