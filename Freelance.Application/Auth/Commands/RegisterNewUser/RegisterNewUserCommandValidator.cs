using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.RegisterNewUser {
    internal class RegisterNewUserCommandValidator: AbstractValidator<RegisterNewUserCommand> {
        public RegisterNewUserCommandValidator() {
            RuleFor(newUser => newUser.Password).NotEmpty().MinimumLength(8).MaximumLength(256).WithName("Пароль");
            RuleFor(newUser => newUser.Login).NotEmpty().MinimumLength(3).MaximumLength(100).WithName("Логин");
            RuleFor(newUser => newUser.Role).NotEmpty().MinimumLength(3).MaximumLength(100).WithName("Роль");

            RuleFor(newUser => newUser.FirstName).NotEmpty()
                .WithName("Имя")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(newUser => newUser.LastName).NotEmpty()
                .WithName("Фамилия")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(newUser => newUser.Email).NotEmpty().MaximumLength(200).EmailAddress()
                .WithName("Email адрес");
        }
    }
}
