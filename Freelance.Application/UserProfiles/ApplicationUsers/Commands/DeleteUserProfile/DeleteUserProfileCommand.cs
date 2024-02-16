using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Commands.DeleteUserProfile {
    public class DeleteUserProfileCommand : IRequest<Unit> {
        public Guid UserId { get; set; }
    }
}
