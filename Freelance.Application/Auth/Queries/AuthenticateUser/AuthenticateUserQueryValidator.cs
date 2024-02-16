using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Queries.AuthenticateUser {
    internal class AuthenticateUserQueryValidator: AbstractValidator<AuthenticateUserQuery> {
        public AuthenticateUserQueryValidator() {
            RuleFor(newUser => newUser.Password).NotEmpty().MinimumLength(8).MaximumLength(256);
            RuleFor(newUser => newUser.Login).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
