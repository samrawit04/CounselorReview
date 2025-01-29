using MongoDB.Driver;
using counselorReview.Models; // Adjust the namespace if necessary

namespace counselorReview.Services
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        // Constructor that accepts the database
        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        // Expose the Clients collection
        public IMongoCollection<Client> Clients => _database.GetCollection<Client>("Clients");

        // Add other collections as needed
    }
}