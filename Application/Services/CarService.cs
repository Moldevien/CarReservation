using Domain.Interfaces;
using TransportRental.Models;

namespace Application.Services
{
    public class CarService
    {
        private readonly IRepository<Car> _carRepository;

        public CarService(IRepository<Car> carRepository) => _carRepository = carRepository;

        public async Task<IEnumerable<Car>> GetAllCarsAsync() => await _carRepository.GetAllAsync();

        public async Task<Car?> GetCarByIdAsync(int id) => await _carRepository.GetByIdAsync(id);

        public async Task AddCarAsync(Car car) => await _carRepository.AddAsync(car);

        public async Task UpdateCarAsync(Car car) => await _carRepository.UpdateAsync(car);

        public async Task DeleteCarAsync(int id) => await _carRepository.DeleteAsync(id);
    }
}
