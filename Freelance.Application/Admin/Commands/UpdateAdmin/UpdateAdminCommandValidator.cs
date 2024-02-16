using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Commands.UpdateAdmin {
    internal class UpdateAdminCommandValidator: AbstractValidator<UpdateAdminCommand> {
        public UpdateAdminCommandValidator() {
            RuleFor(updateImplementerCommand => updateImplementerCommand.AdminId).NotEqual(Guid.Empty);
            RuleFor(updateImplementerCommand => updateImplementerCommand.FirstName)
                .NotEmpty()
                .WithName("Имя")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateImplementerCommand => updateImplementerCommand.LastName)
                .NotEmpty()
                .WithName("Фамилия")
                .Matches("^[a-zA-Zа-яА-Я]*$")
                .MaximumLength(200);
            RuleFor(updateImplementerCommand => updateImplementerCommand.MiddleName)
                .MaximumLength(200).WithName("Отчество");
            RuleFor(updateImplementerCommand => updateImplementerCommand.Birthday)
                .InclusiveBetween(new DateTime(1950, 1, 1), new DateTime(2010, 1, 1))
                .WithName("Дата рождения");
            RuleFor(updateImplementerCommand => updateImplementerCommand.Email)
                .NotEmpty().EmailAddress().MaximumLength(300).WithName("Email");
            RuleFor(updateImplementerCommand => updateImplementerCommand.Phone)
                .NotEmpty().MinimumLength(10).MaximumLength(20).WithName("Номер телефона");
        }
    }
}
