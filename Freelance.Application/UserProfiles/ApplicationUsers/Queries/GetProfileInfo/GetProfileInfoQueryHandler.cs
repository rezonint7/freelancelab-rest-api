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

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo {
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
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var userRole = await _userService.GetUserRoleByIdAsync(request.UserId, cancellationToken);

            if(user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            if(userRole == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            if(userRole.Contains("CUSTOMER")) {
                var customer = await _freelanceDBContext.Customers.FirstOrDefaultAsync(customer => customer.UserId == request.UserId, cancellationToken);
                if(customer == null) { throw new NotFoundException(nameof(Customer), request.UserId); }
                return _mapper.Map<CustomerInfoViewModel>(customer);
            }
            else if(userRole.Contains("IMPLEMENTER")) {
                var impl = await _freelanceDBContext.Implementers.FirstOrDefaultAsync(impl => impl.UserId == request.UserId, cancellationToken);
                if (impl == null) { throw new NotFoundException(nameof(Implementer), request.UserId); }
                return _mapper.Map<ImplementerInfoViewModel>(impl);
            }
            else { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            
        }
    }
}
