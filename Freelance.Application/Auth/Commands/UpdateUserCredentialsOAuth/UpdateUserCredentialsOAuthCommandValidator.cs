using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.UpdateUserCredentialsOAuth {
    internal class UpdateUserCredentialsOAuthCommandValidator: AbstractValidator<UpdateUserCredentialsOAuthCommand> {
        public UpdateUserCredentialsOAuthCommandValidator(){
            RuleFor(updateUser => updateUser.UserId).NotEqual(Guid.Empty);
            RuleFor(updateUser => updateUser.Password).NotEmpty().MinimumLength(8).MaximumLength(256).WithName("Пароль");
            RuleFor(updateUser => updateUser.Login).NotEmpty().MinimumLength(3).MaximumLength(100).WithName("Логин");
            RuleFor(updateUser => updateUser.Role).NotEmpty().MinimumLength(3).MaximumLength(100).WithName("Роль");

            RuleFor(updateUser => updateUser.FirstName).NotEmpty()
                .WithName("Имя")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateUser => updateUser.LastName).NotEmpty()
                .WithName("Фамилия")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateUser => updateUser.Email).NotEmpty().MaximumLength(200).EmailAddress()
                .WithName("Email адрес");
        }
    }
}
