using AutoMapper;
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

namespace Freelance.Application.Admin.Queries.GetDetailsAdmin {
    internal class GetDetailsAdminQueryHandler : IRequestHandler<GetDetailsAdminQuery, AdminDetailsViewModel> {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetDetailsAdminQueryHandler(
            IUserService userService,
            IMapper mapper)
            => (_userService, _mapper) = (userService, mapper);

        public async Task<AdminDetailsViewModel> Handle(GetDetailsAdminQuery request, CancellationToken cancellationToken) {
            var admin = (await _userService.GetUsersAsync("ADMIN", cancellationToken)).FirstOrDefault(adm => adm.Id == request.AdminId);
            
            if (admin == null) {
                throw new NotFoundException(nameof(ApplicationUser), request.AdminId);
            }
            if(admin == null) throw new NotFoundException(nameof(ApplicationUser), request.AdminId);

            return _mapper.Map<AdminDetailsViewModel>(admin);
        }
    }
}
