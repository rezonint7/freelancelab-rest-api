using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Admin.Queries.GelAllAdmins;
using Freelance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GetAllUsers {
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UsersViewModel> {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(
            IUserService userService,
            IMapper mapper)
            => (_userService, _mapper) = (userService, mapper);

        public async Task<UsersViewModel> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) {
            var customers = await _userService.GetUsersAsync("CUSTOMER", cancellationToken);
            var implementers = await _userService.GetUsersAsync("IMPLEMENTER", cancellationToken);

            var customersDto = customers
                .AsQueryable()
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            var implementersDto = implementers
                .AsQueryable()
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new UsersViewModel { Implementers = implementersDto, Customers = customersDto };
        }
    }
}
