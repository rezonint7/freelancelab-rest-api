using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.ResponsesImplOrders.Commands.CreateResponseImplementer {
    internal class CreateNewResponseCommandValidator: AbstractValidator<CreateNewResponseCommand> {
        public CreateNewResponseCommandValidator() {
            RuleFor(createNewResponse => createNewResponse.OrderId).NotEqual(Guid.Empty);
            RuleFor(createNewResponse => createNewResponse.ImplementerId).NotEqual(Guid.Empty);
            RuleFor(createNewResponse => createNewResponse.ResponseMessage).MinimumLength(10).MaximumLength(3000);
        }
    }
}
