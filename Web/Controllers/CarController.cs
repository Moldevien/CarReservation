using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars() => Ok(await _carService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            await _carService.AddAsync(car);
            return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
        }
    }
}
