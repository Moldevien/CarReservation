using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ClientService : Service<Client>
    {
        public ClientService(IRepository<Client> repository) : base(repository)
        {

        }
    }
}
