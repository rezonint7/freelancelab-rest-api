using Freelance.Application.Auth.Commands.RegisterNewUser;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Interfaces {
    public interface IAuthenticationService {
        public Task<Guid> RegisterUserAsync(RegisterNewUserCommand registerNewUserCommand, CancellationToken cancellationToken);
        public Task<string> Authenticate(string login, string password);
        public Task<string> AuthorizationUserWithOAuth(string provider, string accesToken);
        public Task<bool> ResetPassword(string login, string newPassword);
        public Task<bool> Logout();
    }
}
