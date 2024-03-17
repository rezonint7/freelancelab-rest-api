﻿using AutoMapper;
using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.Application.Auth.Queries.AuthenticateUser;
using Freelance.Application.Interfaces;
using Freelance.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public AuthController(IMapper mapper, IJwtService jwtService) => (_mapper, _jwtService) = (mapper, jwtService);

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto loginUser)
        {
            var query = new AuthenticateUserQuery
            {
                Login = loginUser.Login,
                Password = loginUser.Password
            };
            var newJWT = await Mediator.Send(query);


            return Ok(newJWT);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> RegisterNewUser([FromBody] RegisterNewUserDto registerNewUserDto)
        {
            var command = _mapper.Map<RegisterNewUserCommand>(registerNewUserDto);
            var userId = await Mediator.Send(command);

            return Created($"{userId}", userId);
        }


    }
}
