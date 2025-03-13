using TransportRental.Infrastructure.Data;
using TransportRental.Models;

namespace Infrastructure.Repositories
{
    public class CarRepository : Repository<Car>
    {
        public CarRepository(ApplicationDbContext context) : base(context) { }
    }
}
