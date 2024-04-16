using AspNet.Security.OAuth.GitHub;
using Freelance.Application.Auth.Commands.AuthenticateUserOAuth;
using Freelance.WebApi.Controllers.API;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelance.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthUserController : Controller
    {
        private IMediator? _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices
                .GetService<IMediator>() ?? throw new Exception("IMediator service is not available.");

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


        [HttpGet("auth/{provider}")]
        public IActionResult OAuth(string provider) {
            var properties = new AuthenticationProperties {
                RedirectUri = "/oauth-handler/" + provider
            };
            return Challenge(properties, getProviderNormalized(provider));
        }


        [HttpGet("oauth-handler/{provider}")]
        [Authorize(AuthenticationSchemes = GitHubAuthenticationDefaults.AuthenticationScheme + "," + GoogleDefaults.AuthenticationScheme)] // можно задать несколько схем
        public async Task<IActionResult> OAuthHandler(string provider) {
            string token = string.Empty;
            var result = await HttpContext.AuthenticateAsync(getProviderNormalized(provider));
            if (result.Succeeded) {
                var tokens = result.Properties.GetTokens();
                var accessToken = tokens.FirstOrDefault(t => t.Name == "access_token")?.Value;
                var providerNormalized = getProviderNormalized(provider);

                switch (getProviderNormalized(provider)) {
                    case "GitHub": {
                            var command = new AuthenticateUserOAuthCommand {
                                Login = User.FindFirstValue(ClaimTypes.Name)!,
                                Email = User.FindFirstValue(ClaimTypes.Email)!,
                                LastName = "",
                                FirstName = "",
                                OAuthProvider = providerNormalized,
                                OAuthToken = accessToken,
                                OAuthKey = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                            };
                            token = await Mediator.Send(command);
                        } break;

                    case "Google": {
                            var command = new AuthenticateUserOAuthCommand {
                                Login = User.FindFirstValue(ClaimTypes.GivenName)!,
                                Email = User.FindFirstValue(ClaimTypes.Email)!,
                                LastName = "",
                                FirstName = "",
                                OAuthProvider = providerNormalized,
                                OAuthToken = accessToken,
                                OAuthKey = User.FindFirstValue(ClaimTypes.Name)!,
                            };
                            token = await Mediator.Send(command);
                        } break;
                }


                Response.Cookies.Append("oauth_token", token);
                ViewData["token"] = token;
                return View();
            }
            else {
                return NotFound("token");
            }
        }

        private string getProviderNormalized(string provider) {
            switch (provider) {
                case "github": return "GitHub";
                case "google": return "Google";
                case "vkontakte": return "Vkontakte";
                default: return "null";
            }
        }
    }
}
