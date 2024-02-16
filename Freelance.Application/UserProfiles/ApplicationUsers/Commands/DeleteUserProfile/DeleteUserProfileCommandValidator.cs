using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.DeleteUserProfile {
    internal class DeleteUserProfileCommandValidator : AbstractValidator<DeleteUserProfileCommand> {
        public DeleteUserProfileCommandValidator() {
            RuleFor(deleteUserProfile => deleteUserProfile.UserId).NotEqual(Guid.Empty);
        }
    }
}
