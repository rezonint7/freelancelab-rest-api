using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.UpdateUserCredentialsOAuth {
    internal class UpdateUserCredentialsOAuthCommandHandler : IRequestHandler<UpdateUserCredentialsOAuthCommand, string> {

        private readonly IAuthenticationService _authenticationService;
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public UpdateUserCredentialsOAuthCommandHandler(IAuthenticationService authenticationService, IFreelanceDBContext freelanceDBContext, IJwtService jwtService, IUserService userService) {
            _authenticationService = authenticationService;
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(UpdateUserCredentialsOAuthCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            var changeCred = await _userService.ChangeUserCredentialsAsync(request, cancellationToken);
            var changeRole = await _userService.ChangeUserRoleAsync(request.UserId, request.Role, cancellationToken);
            if (changeCred == false && changeRole == false) { { throw new Exception("Что-то пошло не так..."); } }
            user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            return await _jwtService.GenerateJwtToken(user, 43200);
        }
    }
}
