using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.Application.Auth.Commands.UpdateUserCredentialsOAuth;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.Services {
    public class UserService : IUserService {
        private readonly FreelanceDBContext _freelanceDBContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(FreelanceDBContext freelanceDBContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager) {
            _freelanceDBContext = freelanceDBContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string UserId {
            get => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string RemoteIpAddress {
            get => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public string GetSalt() => DateTime.UtcNow.ToString() + DateTime.Now.Ticks;

        public async Task<ApplicationUser?> GetUserByLoginAsync(string login, CancellationToken cancellationToken) {
            return await _freelanceDBContext.Users.FirstOrDefaultAsync(user => user.UserName == login, cancellationToken);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken) {
            return await _freelanceDBContext.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }

        public async Task<ApplicationUser?> GetUserByExternalAsync(string loginProvider, string providerKey, CancellationToken cancellationToken) {
            return await _userManager.FindByLoginAsync(loginProvider, providerKey);
        }

        public async Task<List<string>> GetUserRoleByIdAsync(Guid userId, CancellationToken cancellationToken) {
            var user = await GetUserByIdAsync(userId, cancellationToken);
            if (user != null) {
                var userRoles = await _freelanceDBContext.UserRoles
                    .Where(role => role.UserId == userId)
                    .Select(role => role.RoleId)
                    .ToListAsync(cancellationToken);

                var roleNames = await _freelanceDBContext.Roles
                    .Where(role => userRoles.Contains(role.Id))
                    .Select(role => role.NormalizedName)
                    .ToListAsync(cancellationToken);

                return roleNames;
            }
            return null;
        }

        public async Task<bool> DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken) {
            var user = await GetUserByIdAsync(userId, cancellationToken);
            var userRoles = await GetUserRoleByIdAsync(userId, cancellationToken);

            if (user != null) {
                _freelanceDBContext.Users.Remove(user);

                if (userRoles.Contains("IMPLEMENTER")) {
                    var implementer = await _freelanceDBContext.Implementers
                        .FirstOrDefaultAsync(impl => impl.UserId == userId, cancellationToken);
                    if (implementer != null) {
                        _freelanceDBContext.Implementers.Remove(implementer);
                    }
                }
                else if (userRoles.Contains("CUSTOMER")) {
                    var customer = await _freelanceDBContext.Customers
                        .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
                    if (customer != null) {
                        _freelanceDBContext.Customers.Remove(customer);
                    }
                }

                await _freelanceDBContext.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(string roleName, CancellationToken cancellationToken) {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return usersInRole.ToList();
        }

        public async Task<List<ApplicationUser>> GetAdminsAsync(CancellationToken cancellationToken) {
            return await GetUsersAsync("ADMIN", cancellationToken);
        }

        public async Task<bool> ChangeUserRoleAsync(Guid userId, string roleNormalized, CancellationToken cancellationToken) {
            var user = await GetUserByIdAsync(userId, cancellationToken);
            var role = await _freelanceDBContext.Roles.FirstOrDefaultAsync(role => role.NormalizedName == roleNormalized, cancellationToken);
            var userRole = (await GetUserRoleByIdAsync(userId, cancellationToken)).FirstOrDefault();

            if (role == null) { throw new NotFoundException("Role", roleNormalized); }
            if (user != null) {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            return false;
        }

        public async Task<bool> ChangeUserCredentialsAsync(UpdateUserCredentialsOAuthCommand updateUserCredentials, CancellationToken cancellationToken) {
            var user = await GetUserByIdAsync(updateUserCredentials.UserId, cancellationToken);
            var role = await _freelanceDBContext.Roles.FirstOrDefaultAsync(role => role.NormalizedName == updateUserCredentials.Role, cancellationToken);
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), updateUserCredentials.UserId); }

            await _userManager.AddPasswordAsync(user, updateUserCredentials.Password);

            user.Email = updateUserCredentials.Email;
            user.UserName = updateUserCredentials.Login;
            user.FirstName = updateUserCredentials.FirstName;
            user.LastName = updateUserCredentials.LastName;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;

            var workExperience = await _freelanceDBContext.WorkExperience.FirstAsync();
            var category = await _freelanceDBContext.Categories.FirstAsync();
            if (role.NormalizedName == "IMPLEMENTER") {
                user.Implementers.Add(new Implementer {
                    UserId = user.Id,
                    Specialization = "",
                    WorkExperience = workExperience,
                    Skills = "",
                    Category = category
                });
            }
            else if (role.NormalizedName == "CUSTOMER") {
                user.Customers.Add(new Customer {
                    UserId = user.Id,
                    IsTrusted = false,
                });
            }

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
