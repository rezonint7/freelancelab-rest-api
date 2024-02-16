using AutoMapper;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Commands.DeletePortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Queries.GetDetailsPortfolioItem;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.DeleteUserProfile;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo;
using Freelance.Domain;
using Freelance.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API {

    [Route("api/profile")]
    public class ProfileController : BaseController {
        private readonly IMapper _mapper;
        public ProfileController(IMapper mapper) => _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProfileInfo(Guid id) {
            var query = new GetProfileInfoQuery { UserId = id };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "IMPLEMENTER, CUSTOMER, MANAGER, ADMIN, OWNER")]
        public async Task<ActionResult<Unit>> UdpateProfileInfo([FromBody] UpdateProfileInfoDto updateProfileDto) {
            var command = _mapper.Map<UpdateUserProfileCommand>(updateProfileDto);
            if (!RoleUser.Contains("OWNER") && !RoleUser.Contains("ADMIN")) command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("/delete/{id}")]
        [Authorize(Roles = "Implementer, Admin, Owner")]
        public async Task<ActionResult<Unit>> DeleteUserProfile(Guid id) {

            if(!IsCurrentUserOwnerOrAdmin(UserId, "ADMIN")) return Forbid();
            var command = new DeleteUserProfileCommand { UserId = id };

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("portfolio/details/{id}")]
        public async Task<ActionResult<PortfolioItemViewModel>> GetDetailsPortfolioItem(int id) {
            var query = new GetDetailsPortfolioItemQuery {
                ImplementerId = UserId,
                PortfolioItemId = id
            };
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpPost("portfolio/create")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> CreateNewPortfolioItem([FromBody] CreatePortfolioItemDto createPortfolioItem) {
            var command = _mapper.Map<CreateNewPortfolioItemCommand>(createPortfolioItem);
            command.ImplementerId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("portfolio/edit")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> UpdatePortfolioItem([FromBody] UpdatePortfolioItemDto updatePortfolioItem) {
            var command = _mapper.Map<UpdatePortfolioItemCommand>(updatePortfolioItem);
            command.ImplementerId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("portfolio/delete/{id}")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> DeletePortfolioItem(int id) {
            var command = new DeletePortfolioItemCommand {
                ImplementerId = UserId,
                PortfolioItemId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
