using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Interfaces;
using Freelance.Application.Orders.Queries.GetOrderList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.References.Queries.GetAllReferences {
    internal class GetAllReferencesQueryHandler : IRequestHandler<GetAllReferencesQuery, ReferencesViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetAllReferencesQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper) =>
            (_freelanceDBContext, _mapper) = (freelanceDBContext, mapper);

        public async Task<ReferencesViewModel> Handle(GetAllReferencesQuery request, CancellationToken cancellationToken) {
            var categories = await _freelanceDBContext.Categories
                .ProjectTo<CategoryReferenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var workExperience = await _freelanceDBContext.WorkExperience
                .ProjectTo<WorkExperienceReferenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var currencies = await _freelanceDBContext.Currencies
                .ProjectTo<CurrencyReferenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ReferencesViewModel {Categories = categories, WorkExperience = workExperience, Currencies = currencies};
        }
    }
}
