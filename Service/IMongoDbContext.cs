using MongoDB.Driver;
using counselorReview.Models;  // Ensure this is included to reference the Client model

namespace counselorReview.Services
{
    public interface IMongoDbContext
    {
        IMongoCollection<Client> Clients { get; }
        // Add other collections as needed, e.g.:
        // IMongoCollection<OtherModel> OtherModels { get; }
    }
}