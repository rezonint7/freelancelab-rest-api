using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Files.Commands.UploadFiles {
    public class UploadFilesCommand: IRequest<List<string>> {
        public Guid UserId { get; set; }
        public string BaseUrl { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
