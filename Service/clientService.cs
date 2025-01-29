using counselorReview.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public class ClientService 
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientService(IMongoDatabase database)
        {
            _clientCollection = database.GetCollection<Client>("Clients");
        }

    }
}
