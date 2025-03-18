using TransportRental.Infrastructure.Data;
using TransportRental.Models;

namespace Infrastructure.Repositories
{
    class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}
