using counselorReview.Models;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace counselorReview.Services
{
    public class FeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbacks;

            public FeedbackService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _feedbacks = database.GetCollection<Feedback>("Feedbacks");
        }


    }
}
