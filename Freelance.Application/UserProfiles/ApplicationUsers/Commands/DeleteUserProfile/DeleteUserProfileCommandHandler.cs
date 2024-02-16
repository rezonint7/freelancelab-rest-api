using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.DeleteUserProfile {
    internal class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public DeleteUserProfileCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken) {
            var isDelete = await _userService.DeleteUserByIdAsync(request.UserId, cancellationToken);
            if (!isDelete) { throw new NotFoundException(nameof(ApplicationUser), request.UserId.ToString()); }

            return Unit.Value;
        }
    }
}
