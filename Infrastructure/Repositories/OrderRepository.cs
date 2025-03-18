using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}
