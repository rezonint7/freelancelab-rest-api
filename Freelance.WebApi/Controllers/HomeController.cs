using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller {
        [HttpGet("/")]
        public IActionResult Index() {
            return View();
        }
        [HttpGet("/order/details/{orderId}")]
        public IActionResult DetailsOrder(string orderId) {
            ViewData["orderId"] = orderId;
            return View();
        }
    }
}
