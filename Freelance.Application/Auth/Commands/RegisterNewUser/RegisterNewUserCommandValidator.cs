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
            RuleFor(newUser => newUser.MiddleName).MaximumLength(200)
                .WithName("Отчество")
                .Matches("^[a-zA-Zа-яА-Я]*$");
            RuleFor(newUser => newUser.Email).NotEmpty().MaximumLength(200).EmailAddress()
                .WithName("Email адрес");
            RuleFor(newUser => newUser.Phone).MaximumLength(30)
                .WithName("Номер телефона")
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
        }
    }
}
