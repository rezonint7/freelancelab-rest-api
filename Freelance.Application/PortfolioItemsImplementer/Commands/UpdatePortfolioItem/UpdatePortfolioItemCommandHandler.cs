using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem {
    internal class UpdatePortfolioItemCommandHandler: IRequestHandler<UpdatePortfolioItemCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public UpdatePortfolioItemCommandHandler(IFreelanceDBContext freelanceDBContext) =>
            _freelanceDBContext = freelanceDBContext;

        public async Task<Unit> Handle(UpdatePortfolioItemCommand request, CancellationToken cancellationToken) {
            var impl = await _freelanceDBContext.Implementers.FindAsync(request.ImplementerId, cancellationToken);
            var category = await _freelanceDBContext.Categories.FindAsync(request.CategoryId, cancellationToken);
            var portfolioItem = await _freelanceDBContext.PortfolioItems.FirstOrDefaultAsync(pi => pi.Id == request.PortfolioItemId);
            if (portfolioItem == null || request.ImplementerId != portfolioItem.Implementer.UserId) { throw new NotFoundException(nameof(PortfolioItem), request.PortfolioItemId); }
            if (impl == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }
            if (category == null) { throw new NotFoundException(nameof(Category), request.CategoryId); }

            portfolioItem.Title = request.Title;
            portfolioItem.Description = request.Description;
            portfolioItem.PhotoPath = request.PhotoPath;
            portfolioItem.Category = category;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
