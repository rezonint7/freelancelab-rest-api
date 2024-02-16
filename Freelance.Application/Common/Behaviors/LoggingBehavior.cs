using Freelance.Application.Auth.Queries.AuthenticateUser;
using Freelance.Application.Interfaces;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Common.Behaviors {
    public class LoggingBehavior<TReqest, TResponse> : IPipelineBehavior<TReqest, TResponse> where TReqest : IRequest<TResponse> {
        private readonly IUserService _userService;

        public LoggingBehavior(IUserService userService){
            _userService = userService;
        }
        private string GetSHA256(string password, string salt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = SHA256.HashData(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public async Task<TResponse> Handle(TReqest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
            var requestName = typeof(TReqest).Name;
            var userId = _userService.UserId;
            var remoteIpAddress = _userService.RemoteIpAddress;
            switch (requestName) {
                case "GetOrderListQuery": break;
                case "GetAllReferencesQuery": break;
                case "GetOrderDetailsQuery": break;
                case "AuthenticateUserQuery":
                    Log.Information("{Name} [{@remoteIpAddress}] [USER: {@Login}]", 
                        requestName, 
                        remoteIpAddress,
                        (request as AuthenticateUserQuery).Login);
                    break;
                default:
                    Log.Information("{Name} {@UserId} {@Request}", requestName, userId, request);
                    break;
            }
            var response = await next();
            return response;
        }
    }
}
