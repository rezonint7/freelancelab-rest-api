using Freelance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OrdersCustomerController : Controller {
        private readonly IJwtService _jwtService;

        public OrdersCustomerController(IJwtService jwtService) => _jwtService = jwtService;

        [HttpGet("/order/create")]
        public IActionResult CreateOrder() {
            return View();
        }
        [HttpGet("/order/update/{id}")]
        public IActionResult UpdateOrder(string id) {
            ViewData["orderId"] = id;
            return View();
        }

        [HttpGet("view/order/new")]
        [Authorize(Roles = "Customer")]
        public IActionResult CreateNewOrder() {
            return View();
        }
        [HttpGet("view/order/update/{id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult UpdateNewOrder(string id) {
            ViewData["orderId"] = id;
            return View();
        }
    }
}
