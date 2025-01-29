using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace counselorReview.Models
{
    public class Counselor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string? FullName { get; set; }

        [BsonRequired]
        public string? Email { get; set; }

         public string Password { get; set; } = string.Empty;
        [BsonRequired]
        public List<string> Specializations { get; set; } = new List<string>();
    }
}