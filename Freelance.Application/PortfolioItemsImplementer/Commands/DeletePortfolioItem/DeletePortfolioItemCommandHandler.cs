using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.DeletePortfolioItem {
    internal class DeletePortfolioItemCommandHandler: IRequestHandler<DeletePortfolioItemCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public DeletePortfolioItemCommandHandler(IFreelanceDBContext freelanceDBContext) =>
            _freelanceDBContext = freelanceDBContext;

        public async Task<Unit> Handle(DeletePortfolioItemCommand request, CancellationToken cancellationToken) {
            var impl = await _freelanceDBContext.Implementers.FindAsync(request.ImplementerId, cancellationToken);
            if (impl == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }

            var portfolioItem = await _freelanceDBContext.PortfolioItems.FirstOrDefaultAsync(pi => pi.Id == request.PortfolioItemId);
            impl.Portfolio.Remove(portfolioItem);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
