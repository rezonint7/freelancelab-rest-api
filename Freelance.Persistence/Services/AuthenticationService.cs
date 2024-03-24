using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Freelance.Persistence.Services {
    public class AuthenticationService : IAuthenticationService {
        private readonly FreelanceDBContext _freelanceDBContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthenticationService(UserService userService, JwtService jwtService, FreelanceDBContext freelanceDBContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) {
            _freelanceDBContext = freelanceDBContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<string> Authenticate(string login, string password) {
            var result = await _signInManager.PasswordSignInAsync(login, password, false, lockoutOnFailure: false);

            if (!result.Succeeded) { throw new InvalidUserCredentialsException(); }

            var user = await _userManager.FindByNameAsync(login);
            return await _jwtService.GenerateJwtToken(user);
        }

        public async Task<Guid> RegisterUserAsync(RegisterNewUserCommand registerNewUserCommand, CancellationToken cancellationToken) {
            var userExist = await _userService.GetUserByLoginAsync(registerNewUserCommand.Login, cancellationToken);
            var role = await _freelanceDBContext.Roles.FirstOrDefaultAsync(role => role.NormalizedName == registerNewUserCommand.Role, cancellationToken);
            
            if (userExist != null) { throw new UserAlreadyExistsException(userExist.UserName); }
            if (role == null) { throw new NotFoundException(nameof(Order), registerNewUserCommand.Role); }

            var newUser = new ApplicationUser {
                UserName = registerNewUserCommand.Login,
                Email = registerNewUserCommand.Email,
                FirstName = registerNewUserCommand.FirstName,
                LastName = registerNewUserCommand.LastName,
                MiddleName = registerNewUserCommand.MiddleName,
                AvatarProfilePath = "",
                HeaderProfilePath = "",
                RegisterDate = DateTime.Now,
                About = ""
            };
            var result = await _userManager.CreateAsync(newUser, registerNewUserCommand.Password);
            if (!result.Succeeded) { throw new UserAlreadyExistsException(newUser.Email); } // исправить

            await _userManager.AddToRoleAsync(newUser, role.Name);

            var workExperience = await _freelanceDBContext.WorkExperience.FirstAsync();
            var category = await _freelanceDBContext.Categories.FirstAsync();
            if (role.NormalizedName == "IMPLEMENTER") {
                newUser.Implementers.Add(new Implementer {
                    UserId = newUser.Id,
                    Specialization = "",
                    WorkExperience = workExperience,
                    Skills = "",
                    Category = category
                });
            }
            else if (role.NormalizedName == "CUSTOMER") {
                newUser.Customers.Add(new Customer {
                    UserId = newUser.Id,
                    IsTrusted = false,
                });
            }
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return newUser.Id;
        }

        public async Task<bool> Logout() {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<bool> ResetPassword(string login, string newPassword) {
            var user = await _userManager.FindByNameAsync(login);
            if(user == null) { throw new NotFoundException(nameof(ApplicationUser), login); }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            return result.Succeeded;
        }
    }
}
