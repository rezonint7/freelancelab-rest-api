using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem {
    internal class CreateNewPortfolioItemCommandHandler : IRequestHandler<CreateNewPortfolioItemCommand, int> {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public CreateNewPortfolioItemCommandHandler(IFreelanceDBContext freelanceDBContext) =>
            _freelanceDBContext = freelanceDBContext;


        public async Task<int> Handle(CreateNewPortfolioItemCommand request, CancellationToken cancellationToken) {
            var impl = await _freelanceDBContext.Implementers.FindAsync(request.ImplementerId, cancellationToken);
            var category = await _freelanceDBContext.Categories.FindAsync(request.CategoryId, cancellationToken);
            if (impl == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }
            if (category == null) { throw new NotFoundException(nameof(Category), request.CategoryId); }

            var portfolioItem = new PortfolioItem {
                Title = request.Title,
                Description = request.Description,
                PhotoPath = request.PhotoPath,
                Implementer = impl,
                Category = category,
                CreatedAt = DateTime.Now,
            };

            await _freelanceDBContext.PortfolioItems.AddAsync(portfolioItem, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return portfolioItem.Id;
        }
    }
}
