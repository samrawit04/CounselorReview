using counselorReview.Models;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace counselorReview.Services
{
    public class CounselorService 
    {
        private readonly IMongoCollection<Counselor> _counselors;

        public CounselorService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _counselors = database.GetCollection<Counselor>("Counselors");
        }


    }
}
