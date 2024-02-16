using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile {
    internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public UpdateUserProfileCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var userRole = await _userService.GetUserRoleByIdAsync(request.UserId, cancellationToken);
            if(user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId.ToString()); }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.MiddleName = request.MiddleName;
            user.Birthday = request.Birthday;
            user.About = request.About;
            user.AvatarProfilePath = request.AvatarProfilePath;
            user.HeaderProfilePath = request.HeaderProfilePath;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
