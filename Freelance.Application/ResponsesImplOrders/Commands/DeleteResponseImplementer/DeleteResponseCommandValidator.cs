using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.ResponsesImplOrders.Commands.DeleteResponseImplementer {
    internal class DeleteResponseCommandValidator: AbstractValidator<DeleteResponseCommand> {
        public DeleteResponseCommandValidator() {
            RuleFor(deleteResponse => deleteResponse.ResponseId).NotEqual(Guid.Empty);
            RuleFor(deleteResponse => deleteResponse.ImplementerId).NotEqual(Guid.Empty);
        }
    }
}
