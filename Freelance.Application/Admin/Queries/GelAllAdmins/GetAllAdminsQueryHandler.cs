using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Admin.Queries.GetAllUsers;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Application.Orders.Queries.GetOrderDetails;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GelAllAdmins {
    internal class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, AdminListViewModel> {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllAdminsQueryHandler(
            IUserService userService,
            IMapper mapper)
            => (_userService, _mapper) = (userService, mapper);


        public async Task<AdminListViewModel> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken) {
            var admins = await (await _userService.GetAdminsAsync(cancellationToken))
                .AsQueryable()
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new AdminListViewModel { Admins = admins };
        }
    }
}
