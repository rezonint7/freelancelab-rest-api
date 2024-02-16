using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProfileUserController : Controller {
		[HttpGet("/profile")]
		public IActionResult Index() {
			return View();
		}

        [HttpGet("/profile/edit")]
        public IActionResult Edit() {
            return View();
        }
    }
}
