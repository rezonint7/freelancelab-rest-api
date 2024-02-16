using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CreateOrder {
    internal class CreateNewOrderCommandValidator: AbstractValidator<CreateNewOrderCommand> {
        public CreateNewOrderCommandValidator()
        {
            RuleFor(createOrderCommand => createOrderCommand.CustomerId).NotEqual(Guid.Empty);
            RuleFor(createOrderCommand => createOrderCommand.Title).NotEmpty()
                .MinimumLength(10)
                .WithName("Название заказа")
                .MaximumLength(250);
            RuleFor(createOrderCommand => createOrderCommand.Description).NotEmpty()
                .WithName("Описание заказа").MinimumLength(10).MaximumLength(1000);
            RuleFor(createOrderCommand => createOrderCommand.ProjectFee)
                .GreaterThan(500.0m).WithName("Стоимость заказа");
            RuleFor(createOrderCommand => createOrderCommand.Tags).MaximumLength(700);

            RuleFor(createOrderCommand => createOrderCommand.CategoryId).NotEmpty().InclusiveBetween(1, 6);
            RuleFor(createOrderCommand => createOrderCommand.CurrencyId).NotEmpty().InclusiveBetween(1, 3);
            
        }
    }
}
