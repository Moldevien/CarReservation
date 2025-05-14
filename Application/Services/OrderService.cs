using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderService : Service<Order>
    {
        public OrderService(IRepository<Order> repository) : base(repository) { }

        // додаткові методи за потреби
    }

}
