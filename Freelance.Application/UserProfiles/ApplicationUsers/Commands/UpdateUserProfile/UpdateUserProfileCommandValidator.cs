using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile {
    internal class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand> {
        public UpdateUserProfileCommandValidator() {
            RuleFor(updateUserProfile => updateUserProfile.UserId).NotEqual(Guid.Empty);
            RuleFor(updateUserProfile => updateUserProfile.FirstName)
                .NotEmpty()
                .WithName("Имя")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateUserProfile => updateUserProfile.LastName)
                .NotEmpty()
                .WithName("Фамилия")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateUserProfile => updateUserProfile.MiddleName)
                .MaximumLength(200).Matches("^[a-zA-Zа-яА-Я]*$").WithName("Отчество");
            RuleFor(updateImplementerCommand => updateImplementerCommand.Birthday)
                .InclusiveBetween(new DateTime(1950, 1, 1), DateTime.Now.AddYears(-14))
                .WithName("Дата рождения");
            RuleFor(updateUserProfile => updateUserProfile.About)
                .MaximumLength(1000).WithName("О себе");
            RuleFor(updateUserProfile => updateUserProfile.AvatarProfilePath)
                .MaximumLength(1000).WithName("Аватар профиля");
            RuleFor(updateUserProfile => updateUserProfile.HeaderProfilePath)
                .MaximumLength(1000).WithName("Шапка профиля");
        }
    }
}
