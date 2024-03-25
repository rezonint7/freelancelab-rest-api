using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.SetImplementerToOrder
{
    internal class SetImplementerToOrderValidator : AbstractValidator<SetImplementerToOrderCommand>
    {
        public SetImplementerToOrderValidator()
        {
            RuleFor(createNewResponse => createNewResponse.OrderId).NotEqual(Guid.Empty);
            RuleFor(createNewResponse => createNewResponse.ImplementerId).NotEqual(Guid.Empty);
        }
    }
}
