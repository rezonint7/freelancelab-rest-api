using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile {
    public class UpdateUserProfileCommand : IRequest<Unit> {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? About { get; set; }

        public IFormFile? AvatarFile { get; set; }
        public IFormFile? HeaderFile { get; set; }
    }
}
