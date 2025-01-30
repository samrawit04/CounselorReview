using counselorReview.DTO;
using counselorReview.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(string id);
        Task<Client?> UpdateClientAsync(string id, Client updatedClient);
        Task<bool> DeleteClientAsync(string id);
        Task<Client> RegisterClientAsync(Client client);
         
    }
}
