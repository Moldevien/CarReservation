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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllAsync();
            return View(cars);
        }

        // Форма додавання авто
        [HttpGet]
        public IActionResult Create() => View();

        // Обробка додавання авто
        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddAsync(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // Форма редагування авто
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // Обробка редагування
        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdateAsync(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // Перегляд деталей
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // Видалення авто
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }

}
