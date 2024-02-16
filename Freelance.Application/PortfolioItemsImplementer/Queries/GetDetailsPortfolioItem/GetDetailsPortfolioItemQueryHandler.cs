using AutoMapper;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Queries.GetDetailsPortfolioItem {
    internal class GetDetailsPortfolioItemQueryHandler : IRequestHandler<GetDetailsPortfolioItemQuery, PortfolioItemViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetDetailsPortfolioItemQueryHandler(
            IFreelanceDBContext freelanceDBContext,
            IMapper mapper)
            => (_freelanceDBContext, _mapper) = (freelanceDBContext, mapper);
        public async Task<PortfolioItemViewModel> Handle(GetDetailsPortfolioItemQuery request, CancellationToken cancellationToken) {
            var portfolioItem = await _freelanceDBContext.PortfolioItems.Include(i => i.Implementer).FirstOrDefaultAsync(item => item.Id == request.PortfolioItemId, cancellationToken);
            if(portfolioItem == null) { throw new NotFoundException(nameof(PortfolioItem), request.PortfolioItemId); }
            if (portfolioItem.Implementer.UserId != request.ImplementerId) { throw new NotFoundException(nameof(PortfolioItem), request.PortfolioItemId); }
            
            return _mapper.Map<PortfolioItemViewModel>(portfolioItem);
        }
    }
}
