using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Admin;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(OrderService orderService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Перелік
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        #endregion

        #region Редагування
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                AllRoles = allRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string[] selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Не вдалося оновити користувача.");
                return View(model);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = selectedRoles.Except(userRoles);
            var removedRoles = userRoles.Except(selectedRoles);

            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            await _userManager.AddToRolesAsync(user, addedRoles);

            return RedirectToAction("Index");
        }
        #endregion

        #region Видалення
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Перелік усіх замовлень
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllWithCarAndUserAsync();
            return View(orders);
        }
        #endregion

        #region Підтвердження замовлення
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order != null)
            {
                order.StatusId = 2; // Підтверджено
                await _orderService.UpdateAsync(order);
            }
            return RedirectToAction("Orders");
        }
        #endregion

        #region Відхилення замовлення
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order != null)
            {
                order.StatusId = 3; // Відхилено
                await _orderService.UpdateAsync(order);
            }
            return RedirectToAction("Orders");
        }
        #endregion
    }
}
