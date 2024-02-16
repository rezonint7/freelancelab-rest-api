using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Interfaces;
using Freelance.Application.Orders.Queries.GetOrderList;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList {
    internal class GetImplementerListQueryHandler : IRequestHandler<GetImplementerListQuery, ImplementerListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetImplementerListQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
        }

        public async Task<ImplementerListViewModel> Handle(GetImplementerListQuery request, CancellationToken cancellationToken) {
            IQueryable<Implementer> implQuery = _freelanceDBContext.Implementers;
            if (!string.IsNullOrEmpty(request.Search)) {
                implQuery = implQuery.Where(
                    impl => impl.Skills.ToLower().Contains(request.Search.ToLower()) ||
                    impl.Specialization.ToLower().Contains(request.Search.ToLower()) ||
                    impl.User.About.ToLower().Contains(request.Search.ToLower()));
            }
            if (request.Category != -1) {
                implQuery = implQuery.Where(impl => impl.CategoryId == request.Category);
            }

            int totalItems = await implQuery.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            implQuery = implQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var implementers = await implQuery
                .ProjectTo<ImplementerLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ImplementerListViewModel {
                Implementers = implementers,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
