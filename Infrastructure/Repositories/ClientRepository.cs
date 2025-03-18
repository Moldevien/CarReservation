using TransportRental.Infrastructure.Data;
using TransportRental.Models;

namespace Infrastructure.Repositories
{
    class ClientRepository : Repository<Client>
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }
    }
}
