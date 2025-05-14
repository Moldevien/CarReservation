using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService : Service<Order>
    {
        public OrderService(IRepository<Order> repository) : base(repository) { }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _Repository.Query()
                .Include(o => o.Car)
                .Include(o => o.Status)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllWithCarAndUserAsync()
        {
            return await _Repository.Query()
                .Include(o => o.Car)
                .Include(o => o.Status)
                .Include(o => o.User)
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();
        }

    }
}
