using counselorReview.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public class ClientService :IClientService
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientService(IMongoDatabase database)
        {
            _clientCollection = database.GetCollection<Client>("Clients");
        }
         public async Task<Client> RegisterClientAsync(Client client)
        {
            // Check if the email is already registered
            var existingClient = await _clientCollection.Find(c => c.Email == client.Email).FirstOrDefaultAsync();
            if (existingClient != null)
            {
                throw new Exception("Email is already in use.");
            }

            // Hash password (using BCrypt.Net-Next)
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
            client.CreatedAt = DateTime.UtcNow;

            await _clientCollection.InsertOneAsync(client);
            return client;
        }
    

    }
}
