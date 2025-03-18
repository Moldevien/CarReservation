using Domain.Interfaces;
using TransportRental.Models;

namespace Application.Services
{
    public class ClientService : Service<Client>
    {
        public ClientService(IRepository<Client> repository) : base(repository)
        {

        }
    }
}
