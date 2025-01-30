using counselorReview.Models;
using counselorReview.DTO;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using counselorReview.Configuration;


namespace counselorReview.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbacks;
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<Counselor> _counselors;

        public FeedbackService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _feedbacks = database.GetCollection<Feedback>("Feedbacks");
            _clients = database.GetCollection<Client>("Clients");
            _counselors = database.GetCollection<Counselor>("Counselors");
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

            // Get client and counselor names
            var client = await _clients.Find(c => c.Id == dto.ClientId).FirstOrDefaultAsync();
            var counselor = await _counselors.Find(c => c.Id == dto.CounselorId).FirstOrDefaultAsync();

            return new FeedbackDTO
            {
                Id = feedback.Id,
                ClientId = feedback.ClientId!,
                CounselorId = feedback.CounselorId!,
                ClientName = client?.FullName, // Client's name
                CounselorName = counselor?.FullName, // Counselor's name
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
        }

        // Get All Feedback
        public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
        {
            var feedbacks = await _feedbacks.Find(_ => true).ToListAsync();

            var feedbackDtos = new List<FeedbackDTO>();
            foreach (var feedback in feedbacks)
            {
                var client = await _clients.Find(c => c.Id == feedback.ClientId).FirstOrDefaultAsync();
                var counselor = await _counselors.Find(c => c.Id == feedback.CounselorId).FirstOrDefaultAsync();

                feedbackDtos.Add(new FeedbackDTO
                {
                    Id = feedback.Id,
                    ClientId = feedback.ClientId!,
                    CounselorId = feedback.CounselorId!,
                    ClientName = client?.FullName,
                    CounselorName = counselor?.FullName,
                    Comment = feedback.Comment,
                    CreatedAt = feedback.CreatedAt
                });
            }

            return feedbackDtos;
        }

        // Get Feedback By ID
        public async Task<FeedbackDTO?> GetFeedbackByIdAsync(string id)
        {
            var feedback = await _feedbacks.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (feedback == null)
                return null;

            var client = await _clients.Find(c => c.Id == feedback.ClientId).FirstOrDefaultAsync();
            var counselor = await _counselors.Find(c => c.Id == feedback.CounselorId).FirstOrDefaultAsync();

            return new FeedbackDTO
            {
                Id = feedback.Id,
                ClientId = feedback.ClientId!,
                CounselorId = feedback.CounselorId!,
                ClientName = client?.FullName,
                CounselorName = counselor?.FullName,
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

        // Search Feedback by Client Name
 public async Task<List<FeedbackDTO>> SearchFeedbackByClientNameAsync(string clientName)
{
    // Find all clients matching the client name
var clients = await _clients.Find(c => c.FullName != null && c.FullName.Contains(clientName)).ToListAsync();

    
    var clientIds = clients.Select(c => c.Id).ToList();

    // Find all feedbacks where ClientId matches the found client IDs
    var feedbacks = await _feedbacks.Find(f => clientIds.Contains(f.ClientId)).ToListAsync();

    // Create FeedbackDTOs
    var feedbackDtos = new List<FeedbackDTO>();
    foreach (var feedback in feedbacks)
    {
        var client = await _clients.Find(c => c.Id == feedback.ClientId).FirstOrDefaultAsync();
        var counselor = await _counselors.Find(c => c.Id == feedback.CounselorId).FirstOrDefaultAsync();

        feedbackDtos.Add(new FeedbackDTO
        {
            Id = feedback.Id,
            ClientId = feedback.ClientId,
            CounselorId = feedback.CounselorId,
            ClientName = client?.FullName ?? "Unknown", // Providing fallback for null values
            CounselorName = counselor?.FullName ?? "Unknown", // Providing fallback for null values
            Comment = feedback.Comment,
            CreatedAt = feedback.CreatedAt
        });
    }

    return feedbackDtos;
}


    }
}
