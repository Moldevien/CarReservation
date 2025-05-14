using Domain.Entities;
using Domain.Interfaces;
using System.Security.Claims;

namespace Application.Services
{
    public class CarService : Service<Car>
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) : base(carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetFilteredCarsAsync(CarFilter filter, ClaimsPrincipal user)
        {
            bool isAdmin = user.IsInRole("Admin");
            return await _carRepository.GetFilteredAsync(filter, isAdmin);
        }
    }
}
