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

namespace Freelance.Application.Admin.Commands.UpdateAdmin {
    internal class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public UpdateAdminCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }
        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken) {
            var admin = await _userService.GetUserByIdAsync(request.AdminId, cancellationToken);
            if (admin == null) { throw new NotFoundException(nameof(ApplicationUser), request.AdminId); }

            admin.FirstName = request.FirstName;
            admin.LastName = request.LastName;
            admin.MiddleName = request.MiddleName;
            admin.Birthday = request.Birthday;
            admin.Email = request.Email;
            admin.PhoneNumber = request.Phone;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
