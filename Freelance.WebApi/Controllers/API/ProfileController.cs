﻿using AutoMapper;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Commands.DeletePortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Queries.GetDetailsPortfolioItem;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.DeleteUserProfile;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo;
using Freelance.WebApi.Models.Portfolio;
using Freelance.WebApi.Models.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API
{

    [Route("api/profile")]
    public class ProfileController : BaseController {
        private readonly IMapper _mapper;
        public ProfileController(IMapper mapper) => _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProfileInfo(string id) {
            var query = new GetProfileInfoQuery { UserId = id };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

		[HttpGet("specialists")]
		public async Task<ActionResult<ImplementerListViewModel>> GetListSpecialist(string? search = "", string categories = "-1", int pageSize = 20, int page = 1) {
            var query = new GetImplementerListQuery {
                Search = search,
                Categories = categories,
                PageSize = pageSize,
                Page = page
            };
			var viewModel = await Mediator.Send(query);
			return Ok(viewModel);
		}

        [HttpPut("edit")]
        [Authorize(Roles = "Implementer, Customer, Manager, Admin, Owner")]
        public async Task<ActionResult<Unit>> UdpateProfileInfo([FromForm] UpdateProfileInfoDto updateProfileDto) {
            var command = _mapper.Map<UpdateUserProfileCommand>(updateProfileDto);
            if (!RoleUser.Contains("OWNER") && !RoleUser.Contains("ADMIN")) command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
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
		public async Task<ActionResult<Unit>> CreateNewPortfolioItem([FromForm] CreatePortfolioItemDto createPortfolioItem) {
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
