using Freelance.WebApi.Controllers.API;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthUserController : Controller
    {
        [HttpGet("/auth/login")]
        public IActionResult Login() {
            return View();
        }

        [HttpPost("/auth/login")]
        public IActionResult LoginSuccess() {
            return RedirectToAction("Home", "Index");
        }

        [HttpGet("/auth/register")]
        public IActionResult Registration() {
            return View();
        }
    }
}
