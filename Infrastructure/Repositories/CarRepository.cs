using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class CarRepository : Repository<Car>
    {
        public CarRepository(ApplicationDbContext context) : base(context) { }
    }
}
