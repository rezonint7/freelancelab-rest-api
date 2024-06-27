using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.RegisterNewUser {
    internal class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Guid> {
        private readonly IAuthenticationService _authenticationService;

        public RegisterNewUserCommandHandler(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken) {
            //if (request.Role.ToUpper() == "ADMIN" || request.Role.ToUpper() == "MANAGER") {
            //    throw new NotFoundException("Role", request.Role);
            //}
            return await _authenticationService.RegisterUserAsync(request, cancellationToken);
        }
    }
}
