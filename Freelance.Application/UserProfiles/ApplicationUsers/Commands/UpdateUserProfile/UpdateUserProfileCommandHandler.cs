using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile {
    internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateUserProfileCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService, IWebHostEnvironment webHostEnvironment) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            if (user == null) {
                throw new NotFoundException(nameof(ApplicationUser), request.UserId.ToString());
            }

            var userRole = await _userService.GetUserRoleByIdAsync(request.UserId, cancellationToken);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.MiddleName = request.MiddleName;
            user.Birthday = request.Birthday;
            user.About = request.About;

			var userDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", request.UserId.ToString());
            if (!Directory.Exists(userDirectory)) {
                Directory.CreateDirectory(userDirectory);
            }
            if (request.AvatarFile != null) {
                var avatarPath = Path.Combine(userDirectory, "avatar.png");
                using (var stream = new FileStream(avatarPath, FileMode.Create)) {
                    await request.AvatarFile.CopyToAsync(stream);
                }
                user.AvatarProfilePath = Path.Combine("uploads", request.UserId.ToString(), "avatar.png").Replace('\\', '/');
            }

            if (request.HeaderFile != null) {
                var headerPath = Path.Combine(userDirectory, "header.png");
                using (var stream = new FileStream(headerPath, FileMode.Create)) {
                    await request.HeaderFile.CopyToAsync(stream);
                }
                user.HeaderProfilePath = Path.Combine("uploads", request.UserId.ToString(), "header.png").Replace('\\', '/');
            }

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
