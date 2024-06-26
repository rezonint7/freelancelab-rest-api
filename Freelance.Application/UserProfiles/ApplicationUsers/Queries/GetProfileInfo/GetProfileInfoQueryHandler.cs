﻿using AutoMapper;
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

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo
{
    internal class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery, object> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetProfileInfoQueryHandler(IFreelanceDBContext freelanceDBContext, IUserService userService, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByLoginAsync(request.UserId, cancellationToken);
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            var userRole = await _userService.GetUserRoleByIdAsync(user.Id, cancellationToken);
            if(userRole == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            if(userRole.Contains("CUSTOMER")) {
                var customer = await _freelanceDBContext.Customers
                    .Include(i => i.Orders)
                    .ThenInclude(i => i.Currency)
                    .Include(i => i.User.Feedbacks)
                    .FirstOrDefaultAsync(customer => customer.UserId == user.Id, cancellationToken);
                if(customer == null) { throw new NotFoundException(nameof(Customer), request.UserId); }
                var customerReturn = _mapper.Map<CustomerInfoViewModel>(customer);
                customerReturn.Role = userRole[0];
                return customerReturn;

			}
            else if(userRole.Contains("IMPLEMENTER")) {
                var impl = await _freelanceDBContext.Implementers
                    .Include(i => i.Orders)
                    .ThenInclude(i => i.Currency)
                    .Include(i => i.Portfolio)
                    .Include(i => i.User.Feedbacks)
                    .FirstOrDefaultAsync(impl => impl.UserId == user.Id, cancellationToken);
                if (impl == null) { throw new NotFoundException(nameof(Implementer), request.UserId); }
				var implReturn = _mapper.Map<ImplementerInfoViewModel>(impl);
				implReturn.Role = userRole[0];
                return implReturn;

			}
            else { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            
        }
    }
}
