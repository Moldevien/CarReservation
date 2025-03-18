using Application.Services;
using Microsoft.AspNetCore.Mvc;
using TransportRental.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder() => Ok(await _orderService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            await _orderService.AddAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
    }
}
