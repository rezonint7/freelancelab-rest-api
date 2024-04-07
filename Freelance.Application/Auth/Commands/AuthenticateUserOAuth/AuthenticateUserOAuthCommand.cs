using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Commands.AuthenticateUserOAuth {
    public class AuthenticateUserOAuthCommand: IRequest<string> {
        public string Login { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string OAuthProvider { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthKey { get; set; }
    }
}
