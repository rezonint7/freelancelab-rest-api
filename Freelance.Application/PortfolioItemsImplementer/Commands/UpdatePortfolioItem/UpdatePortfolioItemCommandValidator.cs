using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem {
    internal class UpdatePortfolioItemCommandValidator: AbstractValidator<UpdatePortfolioItemCommand> {
        public UpdatePortfolioItemCommandValidator() {
            RuleFor(createNewPortfolio => createNewPortfolio.ImplementerId).NotEqual(Guid.Empty);
            RuleFor(createNewPortfolio => createNewPortfolio.PortfolioItemId).NotEmpty();
            RuleFor(createNewPortfolio => createNewPortfolio.Title).NotEmpty().MinimumLength(10).MaximumLength(250);
            RuleFor(createNewPortfolio => createNewPortfolio.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);

        }
    }
}
