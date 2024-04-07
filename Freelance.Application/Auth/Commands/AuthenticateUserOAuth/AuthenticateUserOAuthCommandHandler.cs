using Freelance.Application.Auth.Commands.UpdateUserCredentialsOAuth;
using Freelance.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.AuthenticateUserOAuth {
    internal class AuthenticateUserOAuthCommandHandler : IRequestHandler<AuthenticateUserOAuthCommand, string> {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateUserOAuthCommandHandler(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(AuthenticateUserOAuthCommand request, CancellationToken cancellationToken) {
            return await _authenticationService.AuthenticateOAuth(request, cancellationToken);
        }
    }
}
