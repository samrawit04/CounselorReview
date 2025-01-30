using counselorReview.Models;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using counselorReview.DTO;

namespace counselorReview.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbacks;

        public FeedbackService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _feedbacks = database.GetCollection<Feedback>("Feedbacks");
        }

        // Create Feedback
        public async Task<FeedbackDTO> CreateFeedbackAsync(CreateFeedbackDTO dto)
        {
            var feedback = new Feedback
            {
                ClientId = dto.ClientId,
                CounselorId = dto.CounselorId,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _feedbacks.InsertOneAsync(feedback);
            return new FeedbackDTO
            {
                Id = feedback.Id,
                ClientId = feedback.ClientId!,
                CounselorId = feedback.CounselorId!,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
        }

        // Get All Feedback
        public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
        {
            var feedbacks = await _feedbacks.Find(_ => true).ToListAsync();
            return feedbacks.ConvertAll(f => new FeedbackDTO
            {
                Id = f.Id,
                ClientId = f.ClientId!,
                CounselorId = f.CounselorId!,
                Comment = f.Comment,
                CreatedAt = f.CreatedAt
            });
        }

        // Get Feedback By ID
        public async Task<FeedbackDTO?> GetFeedbackByIdAsync(string id)
        {
            var feedback = await _feedbacks.Find(f => f.Id == id).FirstOrDefaultAsync();
            return feedback == null ? null : new FeedbackDTO
            {
                Id = feedback.Id,
                ClientId = feedback.ClientId!,
                CounselorId = feedback.CounselorId!,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
        }

        // Update Feedback
        public async Task<bool> UpdateFeedbackAsync(string id, CreateFeedbackDTO dto)
        {
            var update = Builders<Feedback>.Update
                .Set(f => f.ClientId, dto.ClientId)
                .Set(f => f.CounselorId, dto.CounselorId)
                .Set(f => f.Comment, dto.Comment)
                .Set(f => f.CreatedAt, DateTime.UtcNow);

            var result = await _feedbacks.UpdateOneAsync(f => f.Id == id, update);
            return result.ModifiedCount > 0;
        }

        // Delete Feedback
        public async Task<bool> DeleteFeedbackAsync(string id)
        {
            var result = await _feedbacks.DeleteOneAsync(f => f.Id == id);
            return result.DeletedCount > 0;
        } 
    }
}
