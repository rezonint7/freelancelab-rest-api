using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CompleteOrder
{
    internal class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
    {
        public CompleteOrderCommandValidator()
        {
            RuleFor(createNewResponse => createNewResponse.OrderId).NotEqual(Guid.Empty);
            RuleFor(createNewResponse => createNewResponse.CustomerId).NotEqual(Guid.Empty);
        }
    }
}
