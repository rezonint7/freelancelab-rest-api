using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.DeletePortfolioItem {
    internal class DeletePortfolioItemCommandValidator: AbstractValidator<DeletePortfolioItemCommand>{
        public DeletePortfolioItemCommandValidator() {
            RuleFor(createNewPortfolio => createNewPortfolio.ImplementerId).NotEqual(Guid.Empty);
            RuleFor(createNewPortfolio => createNewPortfolio.PortfolioItemId).NotEmpty();
        }
    }
}
