using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.DeleteOrder {
    internal class DeleteOrderCommandValidator: AbstractValidator<DeleteOrderCommand> {
        public DeleteOrderCommandValidator()
        {
            RuleFor(deleteOrderCommand => deleteOrderCommand.OrderId).NotEqual(Guid.Empty);
            RuleFor(deleteOrderCommand => deleteOrderCommand.CustomerId).NotEqual(Guid.Empty);
        }
    }
}
