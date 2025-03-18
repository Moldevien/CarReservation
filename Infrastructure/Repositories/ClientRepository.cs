using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }
    }
}
