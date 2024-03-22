using Freelance.Application.Interfaces;
using Freelance.Domain;
using Freelance.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence {
    public static class DependencyInjection {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<FreelanceDBContext>(options => {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>(provider =>
            {
                var freelanceDBContext = provider.GetService<FreelanceDBContext>();
                var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
                var signInManager = provider.GetService<SignInManager<ApplicationUser>>();
                var userManager = provider.GetService<UserManager<ApplicationUser>>();

                var userService = new UserService(freelanceDBContext, httpContextAccessor, userManager);
                var jwtService = new JwtService(configuration, userManager);
                return new AuthenticationService(userService, jwtService, freelanceDBContext, signInManager, userManager);
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                // Настройки замены забытого пароля
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<FreelanceDBContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserService>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<IFreelanceDBContext>(provider => provider.GetService<FreelanceDBContext>());

            return services;
        }
    }
}
