using Freelance.Application.Files.Commands.UploadFiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Freelance.WebApi.Controllers.API {
    [Route("api/files")]
    public class FilesController: BaseController {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilesController(IMediator mediator, IHttpContextAccessor httpContextAccessor) {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpPost("upload")]
        [Authorize(Roles = "Implementer, Customer, Manager, Admin, Owner")]
        public async Task<ActionResult<List<string>>> UploadFiles([FromForm] List<IFormFile> files) {
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var command = new UploadFilesCommand { 
                UserId = UserId, 
                Files = files, 
                BaseUrl = baseUrl 
            };
            var uploadedFilePaths = await _mediator.Send(command);

            return uploadedFilePaths;
        }
    }
}
