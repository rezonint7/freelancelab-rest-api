using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminPanelController : Controller {
        [HttpGet("admin/orders")]
        public IActionResult Orders() {
            return View();
        }

        [HttpGet("admin/order/{orderId}")]
        public IActionResult OrderDetails(string orderId) {
            ViewData["orderAdmId"] = orderId;
            return View();
        }

        [HttpGet("admin/order/edit/{orderId}")]
        public IActionResult OrderEdit(string orderId) {
            ViewData["orderAdmId"] = orderId;
            return View();
        }

        [HttpGet("admin/admins/edit/{adminId}")]
        public IActionResult AdminEdit(string adminId) {
            ViewData["adminId"] = adminId;
            return View();
        }

        [HttpGet("admin/users/edit/{userId}")]
        public IActionResult UserEdit(string userId, int roleId) {
            ViewData["userId"] = userId;
            ViewData["roleId"] = roleId;
            return View();
        }

        [HttpGet("admin/admins")]
        public IActionResult Admins() {
            return View();
        }

        [HttpGet("admin/admins/register")]
        public IActionResult AdminRegister() {
            return View();
        }

        [HttpGet("admin/logs")]
        public IActionResult Logs() {
            return View();
        }
        [HttpGet("admin/users")]
        public IActionResult Users() {
            return View();
        }
    }
}
