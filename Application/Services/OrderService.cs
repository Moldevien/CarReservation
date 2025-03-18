using Domain.Interfaces;
using TransportRental.Models;

namespace Application.Services
{
    public class OrderService : Service<Order>
    {
        public OrderService(IRepository<Order> repository) : base(repository)
        {

        }
    }
}
