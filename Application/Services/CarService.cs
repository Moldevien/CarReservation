using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CarService : Service<Car>
    {
        public CarService(IRepository<Car> repository) : base(repository)
        {

        }
    }
}
