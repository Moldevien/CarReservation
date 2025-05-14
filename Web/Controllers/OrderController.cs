using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Order;

namespace Web.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrderController(OrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid();
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(user.Id);

            var viewModel = orders.Select(o => new MyOrderViewModel
            {
                CarName = $"{o.Car.Brand} {o.Car.Model}",
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                Status = o.Status.Name,
                PricePerDay = o.Car.PricePerDay
            });

            return View(viewModel);
        }
    }
}
