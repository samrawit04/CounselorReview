using counselorReview.DTO;
using counselorReview.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public class ClientService : IClientService
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientService(IMongoDatabase database)
        {
            _clients = database.GetCollection<Client>("Clients");
        }
        public async Task<Client> RegisterClientAsync(Client client)
        {
            // Check if the email is already registered
            var existingClient = await _clients.Find(c => c.Email == client.Email).FirstOrDefaultAsync();
            if (existingClient != null)
            {
                throw new Exception("Email is already in use.");
            }

            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
            client.CreatedAt = DateTime.UtcNow;

            await _clients.InsertOneAsync(client);

            client.Password = string.Empty;

            return client;
        }


        public async Task<List<Client>> GetAllClientsAsync() => await _clients.Find(_ => true).ToListAsync();

        public async Task<Client?> GetClientByIdAsync(string id) => await _clients.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task<Client?> UpdateClientAsync(string id, Client updatedClient)
        {
            updatedClient.UpdatedAt = DateTime.UtcNow;
            var result = await _clients.ReplaceOneAsync(c => c.Id == id, updatedClient);
            return result.ModifiedCount > 0 ? updatedClient : null;
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            var result = await _clients.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }

    }
}
