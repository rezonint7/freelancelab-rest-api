using Freelance.Application.Auth.Commands.UpdateUserCredentialsOAuth;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Interfaces {
    public interface IUserService {
        public string UserId { get; }
        public string RemoteIpAddress{ get; }
        public Task<List<ApplicationUser>> GetUsersAsync(string roleName, CancellationToken cancellationToken);
        public Task<List<ApplicationUser>> GetAdminsAsync(CancellationToken cancellationToken);
        public Task<ApplicationUser?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
        public Task<ApplicationUser?> GetUserByExternalAsync(string loginProvider, string providerKey, CancellationToken cancellationToken);
        public Task<ApplicationUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        public Task<List<string>> GetUserRoleByIdAsync(Guid userId, CancellationToken cancellationToken);
        public Task<bool> ChangeUserRoleAsync(Guid userId, string roleNormalized, CancellationToken cancellationToken);
        public Task<bool> ChangeUserCredentialsAsync(UpdateUserCredentialsOAuthCommand updateUserCredentials, CancellationToken cancellationToken);
        public Task<bool> DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
