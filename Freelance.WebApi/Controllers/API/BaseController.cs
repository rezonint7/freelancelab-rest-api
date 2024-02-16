using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelance.WebApi.Controllers.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices
                .GetService<IMediator>() ?? throw new Exception("IMediator service is not available.");

        internal Guid UserId => User?.Identity?.IsAuthenticated == true
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
            : Guid.Empty;

        internal IEnumerable<string> RoleUser => User?.Identity?.IsAuthenticated == true
            ? User.FindAll(ClaimTypes.Role).Select(claim => claim.Value) : new List<string>();

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool IsCurrentUserOwnerOrAdmin (Guid userId, string role = "ADMIN") {
            bool isAdmin = RoleUser.Contains("ADMIN") || RoleUser.Contains("OWNER");
            bool isOwner = UserId.ToString() == userId.ToString();

            return isAdmin || isOwner;
        }
    }
}
