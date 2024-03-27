using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Security.Claims;

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


        [HttpGet("github-oauth")]
        public IActionResult GitHubOAuth() {
            var properties = new AuthenticationProperties {
                RedirectUri = Url.Action(nameof(HandleGitHubResponse), "Home", new { code_challenge = "" }, Request.Scheme)
            };
            return Challenge(properties, "GitHub");
        }

        [HttpGet("github-oauth-handler")]
        public async Task<IActionResult> HandleGitHubResponse() {
            var properties = new AuthenticationProperties {
                RedirectUri = Url.Action(nameof(HandleGitHubResponse), "Home", new { code_challenge = "" }, Request.Scheme)
            };
            var codeResult = Challenge(properties, "GitHub");

            var code = Request.Query["code"].ToString();
            var error = Request.Query["error"].ToString();

            if (!string.IsNullOrEmpty(error)) {
                // Обработка ошибок, если таковые имеются
                return RedirectToAction("Error");
            }

            if (string.IsNullOrEmpty(code)) {
                // Код не получен
                return RedirectToAction("Error-Code");
            }

            // Обменяем полученный код на токен доступа
            var tokenResponse = await ExchangeCodeForAccessToken(code);
            if (!tokenResponse.IsSuccessStatusCode) {
                // Обработка ошибок при обмене кода на токен
                return RedirectToAction("Error");
            }

            // Получаем токен из ответа
            var responseContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseValues = QueryHelpers.ParseQuery(responseContent);
            var accessToken = tokenResponseValues["access_token"];

            // Далее можно сохранить токен и перенаправить пользователя на нужную страницу
            // Например, RedirectToAction("UserProfile", "User", new { accessToken = accessToken });
            // Или просто вернуть его клиенту

            return Ok(new { access_token = accessToken });
        }

        private async Task<HttpResponseMessage> ExchangeCodeForAccessToken(string code) {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            using var httpClient = new HttpClient();

            var tokenRequestParameters = new Dictionary<string, string>
            {
        { "client_id", configuration["OAuth:GitHub:ClientId"] },
        { "client_secret", configuration["OAuth:GitHub:ClientSecret"] },
        { "code", code }
    };

            var tokenRequestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var tokenResponse = await httpClient.PostAsync("https://github.com/login/oauth/access_token", tokenRequestContent);

            return tokenResponse;
        }


        [HttpGet("profile-git")]
        public IActionResult GetUserProfile() {
            // Получение текущего пользователя из контекста HTTP
            var user = HttpContext.User;

            // Проверка, что пользователь аутентифицирован
            if (user.Identity.IsAuthenticated) {
                // Получение идентификационных данных пользователя GitHub из Claims
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = user.FindFirst(ClaimTypes.Name)?.Value;
                var userLogin = user.FindFirst("urn:github:login")?.Value;
                var userProfileUrl = user.FindFirst("urn:github:url")?.Value;
                var userAvatarUrl = user.FindFirst("urn:github:avatar")?.Value;

                // Возвращаем данные о пользователе
                return Ok(new {
                    UserId = userId,
                    UserName = userName,
                    UserLogin = userLogin,
                    UserProfileUrl = userProfileUrl,
                    UserAvatarUrl = userAvatarUrl
                });
            }

            // Возвращаем ошибку, если пользователь не аутентифицирован
            return Unauthorized();
        }
    }
}
