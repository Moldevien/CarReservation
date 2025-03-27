using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;
        public CarController(CarService carService) => _carService = carService;

        // Відображення списку автомобілів
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllAsync();
            return View(cars);
        }

        // Форма додавання авто
        public IActionResult Create() => View();

        // Обробка додавання авто
        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.AddAsync(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // Форма редагування авто
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetByIdAsync(id); // ✅ Чекаємо результат
            if (car == null)
            {
                return NotFound();
            }
            return View(car); // ✅ Передаємо об'єкт Car у View
        }

        // Обробка редагування
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.UpdateAsync(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // Перегляд деталей
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetByIdAsync(id); // ✅ Чекаємо результат
            if (car == null)
            {
                return NotFound();
            }
            return View(car); // ✅ Передаємо об'єкт Car
        }

        // Видалення авто
        public IActionResult Delete(int id)
        {
            _carService.DeleteAsync(id);
            return RedirectToAction("Index");
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
