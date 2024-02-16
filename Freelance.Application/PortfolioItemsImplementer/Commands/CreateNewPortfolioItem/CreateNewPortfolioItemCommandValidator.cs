using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem {
    internal class CreateNewPortfolioItemCommandValidator: AbstractValidator<CreateNewPortfolioItemCommand> {
        public CreateNewPortfolioItemCommandValidator() {
            RuleFor(createNewPortfolio => createNewPortfolio.ImplementerId).NotEqual(Guid.Empty);
            RuleFor(createNewPortfolio => createNewPortfolio.Title).NotEmpty().MinimumLength(10).MaximumLength(250);
            RuleFor(createNewPortfolio => createNewPortfolio.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);
            RuleFor(createNewPortfolio => createNewPortfolio.PhotoPath).MaximumLength(2000);

        }
    }
}
