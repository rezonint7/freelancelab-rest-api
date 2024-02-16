using FluentValidation;
using Freelance.Application.Common.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Freelance.WebApi.Middleware {
    public class CustomExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext) {
            try {
                await _next(httpContext);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex) {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex) {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
                case UserAlreadyExistsException userAlreadyExistsException:
                    code = HttpStatusCode.BadRequest;
                    result = userAlreadyExistsException.Message;
                    break;
                case InvalidUserCredentialsException invalidUserCredentialsException:
                    Log.Warning($"[{httpContext.Connection.RemoteIpAddress}] Invalid UserCredentials Exception - status: {HttpStatusCode.Unauthorized}");
                    code = HttpStatusCode.Unauthorized;
                    result = invalidUserCredentialsException.Message;
                    break;
                case UnauthorizedAccessException unauthorizedAccessException: 
                    code = HttpStatusCode.Unauthorized;
                    result = unauthorizedAccessException.Message;
                    break;
            }
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            if(result == string.Empty) {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }
            return httpContext.Response.WriteAsync(result);
        }
    }
}
