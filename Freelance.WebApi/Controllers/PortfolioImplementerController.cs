using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers {
    public class PortfolioImplementerController : Controller {
        [HttpGet("view/profile/portfolio/create")]
        [Authorize(Roles = "Implementer")]
        public IActionResult CreateNewPortfolioItem() {
            return View();
        }

        [HttpGet("view/profile/portfolio/update")]
        [Authorize(Roles = "Implementer")]
        public IActionResult UpdateNewPortfolioItem(string id) {
            ViewData["portfolioId"] = id;
            return View();
        }

        [HttpGet("profile/portfolio/create")]
        public IActionResult CreatePortfolioItem() {
            return View();
        }
        [HttpGet("profile/portfolio/update")]
        public IActionResult UpdatePortfolioItem(string id) {
            ViewData["portfolioId"] = id;
            return View();
        }
    }
}
