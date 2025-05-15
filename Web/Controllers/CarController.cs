using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Car;
using Web.Models.Order;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;
        private readonly UserManager<User> _userManager;
        private readonly OrderService _orderService;

        public CarController(CarService carService, UserManager<User> userManager, OrderService orderService)
        {
            _carService = carService;
            _userManager = userManager;
            _orderService = orderService;
        }


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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound();

            var model = new BookingViewModel
            {
                CarId = car.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            ViewBag.Car = car;
            return View(model);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Details(BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Car = await _carService.GetByIdAsync(model.CarId);
                return View(model);
            }

            if (model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError("", "Дата завершення має бути після дати початку.");
                ViewBag.Car = await _carService.GetByIdAsync(model.CarId);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); // або RedirectToAction("Login", "Account");
            }

            var order = new Order
            {
                CarId = model.CarId,
                UserId = user.Id,
                StartDate = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(model.EndDate, DateTimeKind.Utc),
                StatusId = 1
            };

            await _orderService.AddAsync(order);

            TempData["Success"] = "Запит на бронювання відправлено!";
            return RedirectToAction("Index", "Car");
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
                return NotFound();

            car.IsAvailable = !car.IsAvailable;
            await _carService.UpdateAsync(car);

            TempData["Success"] = car.IsAvailable
                ? "Автомобіль тепер доступний для користувачів."
                : "Автомобіль зроблено недоступним.";

            return RedirectToAction("Index");
        }

    }
}
