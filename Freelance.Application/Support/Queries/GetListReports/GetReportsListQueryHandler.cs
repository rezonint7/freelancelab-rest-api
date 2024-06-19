using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Interfaces;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Queries.GetListReports {
    internal class GetReportsListQueryHandler : IRequestHandler<GetListReportsQuery, ReportsListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetReportsListQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
        }

        public async Task<ReportsListViewModel> Handle(GetListReportsQuery request, CancellationToken cancellationToken) {
            IQueryable<ReportToSupport> reportsQuery = _freelanceDBContext.ReportsToSupport;

            int totalItems = await reportsQuery.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            reportsQuery = reportsQuery
                .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

            var reports = await reportsQuery
                .ProjectTo<ReportLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ReportsListViewModel {
                Reports = reports,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
