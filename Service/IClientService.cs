using counselorReview.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(string id);
        Task<Client> CreateAsync(Client client);
        Task<bool> UpdateAsync(string id, Client client);
        Task<bool> DeleteAsync(string id);
    }
}
