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

        public Guid UserId { 
            get {
                var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
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
    }
}
