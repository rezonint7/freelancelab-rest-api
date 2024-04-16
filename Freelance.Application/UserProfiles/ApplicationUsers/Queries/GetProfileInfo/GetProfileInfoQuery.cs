using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo {
    public class GetProfileInfoQuery : IRequest<object> {
        public string UserId { get; set; }
    }
}
