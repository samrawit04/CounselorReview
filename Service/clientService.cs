using counselorReview.DTO;
using counselorReview.Models;
using counselorReview.Services;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

public class ClientService : IClientService
{
    private readonly IMongoDbContext _context; // Corrected typo

    public ClientService(IMongoDbContext context)
    {
        _context = context;
    }

    public async Task<Client> SignUpAsync(CreateClientDTO createClientDto)
    {
        // Check if the client already exists
        var existingClient = await _context.Clients
            .Find(c => c.Email == createClientDto.Email)
            .FirstOrDefaultAsync();

        if (existingClient != null)
        {
            throw new InvalidOperationException("A client with this email already exists.");
        }

        // Create a new Client instance
        var newClient = new Client
        {
            Email = createClientDto.Email,
            DateRegistered = DateTime.UtcNow // Ensure this property exists in the Client model
        };

        // Insert the new client into the database
        await _context.Clients.InsertOneAsync(newClient);
        return newClient;
    }
}