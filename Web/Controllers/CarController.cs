using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Car;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;
        public CarController(CarService carService) => _carService = carService;

        #region Список з фільтрацією
        // Відображення списку автомобілів
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CarFilterViewModel filterVm)
        {
            var filter = new CarFilter
            {
                Brand = filterVm.Brand,
                Model = filterVm.Model,
                MinPrice = filterVm.MinPrice,
                MaxPrice = filterVm.MaxPrice,
                Gas = filterVm.Gas,
                Passengers = filterVm.Passengers,
                Volume = filterVm.Volume,
                Gear_box = filterVm.Gear_box
            };

            var cars = await _carService.GetFilteredCarsAsync(filter, User);
            ViewBag.Filter = filterVm;
            return View(cars);
        }
        #endregion

        #region Додавання
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
        #endregion

        #region Редагування
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
        #endregion

        #region Деталі
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
        #endregion

        #region Видалення
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Фільтрування
        /*[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Filter(CarFilterViewModel model)
        {
            var filter = new CarFilter
            {
                Brand = model.Brand,
                Model = model.Model,
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice,
                Gas = model.Gas,
                Passengers = model.Passengers,
                Volume = model.Volume,
                Gear_box = model.Gear_box
            };

            var cars = await _carService.GetFilteredCarsAsync(filter, User);
            return View("Index", cars);
        }*/
        #endregion
    }
}
