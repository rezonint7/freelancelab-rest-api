using AutoMapper;
using Freelance.Application.Admin.Commands.DeleteAdmin;
using Freelance.Application.Admin.Commands.UpdateAdmin;
using Freelance.Application.Admin.Queries.GelAllAdmins;
using Freelance.Application.Admin.Queries.GetAllUsers;
using Freelance.Application.Admin.Queries.GetDetailsAdmin;
using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.WebApi.Models.Admin;
using Freelance.WebApi.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API
{
    [Route("api/admin")]
    public class AdminController: BaseController {
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<AdminListViewModel>> GetAllAdmins() {
            var query = new GetAllAdminsQuery();
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<UsersViewModel>> GetAllUsers() {
            var query = new GetAllUsersQuery();
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<Guid>> RegisterNewAdmin([FromBody] RegisterNewUserDto registerNewUserDto) {
            var command = _mapper.Map<RegisterNewUserCommand>(registerNewUserDto);
            var userId = await Mediator.Send(command);

            return Created($"{userId}", userId);
        }

        [HttpGet("details/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<AdminDetailsViewModel>> GetDetailsAdmin(Guid id) {
            var query = new GetDetailsAdminQuery {
                AdminId = id
            };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<Unit>> UdpateAdmin([FromBody] UpdateAdminDto updateAdminDto) {
            var command = _mapper.Map<UpdateAdminCommand>(updateAdminDto);
            if(RoleUser.Contains("ADMIN")) command.AdminId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<Unit>> DeleteAdmin(Guid id) {
            if (!IsCurrentUserOwnerOrAdmin(id, "Owner")) return Forbid();
            var command = new DeleteAdminCommand { AdminId = id };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
