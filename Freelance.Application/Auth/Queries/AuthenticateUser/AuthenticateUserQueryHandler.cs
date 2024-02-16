using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Queries.AuthenticateUser {
    internal class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, string> {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateUserQueryHandler(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken) {
            return await _authenticationService.Authenticate(request.Login, request.Password);
        }
    }
}
