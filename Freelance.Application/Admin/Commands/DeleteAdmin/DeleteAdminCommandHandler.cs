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

namespace Freelance.Application.Admin.Commands.DeleteAdmin {
    internal class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public DeleteAdminCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }
        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken) {
            var admin = await _userService.GetUserByIdAsync(request.AdminId, cancellationToken);
            var roles = await _userService.GetUserRoleByIdAsync(request.AdminId, cancellationToken);
            if(admin == null) { throw new NotFoundException(nameof(ApplicationUser), request.AdminId); }
            if(roles.Contains("ADMIN")) { throw new NotFoundException("Role", "Admin"); }

            await _userService.DeleteUserByIdAsync(admin.Id, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
