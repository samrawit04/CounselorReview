using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace counselorReview.Models
{
    public class Counselor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonRequired]
        public required string FullName { get; set; }

        [BsonRequired]
        public required string Email { get; set; }


        [BsonRequired]
        public required List<string> Specializations { get; set; } = new List<string>();
    }
}