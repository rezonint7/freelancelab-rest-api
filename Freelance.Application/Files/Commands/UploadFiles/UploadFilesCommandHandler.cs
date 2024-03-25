using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Files.Commands.UploadFiles {
    internal class UploadFilesCommandHandler: IRequestHandler<UploadFilesCommand, List<string>> {
        private readonly string _uploadPath;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadFilesCommandHandler(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
            _uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
        }

        public async Task<List<string>> Handle(UploadFilesCommand request, CancellationToken cancellationToken) {
            var uploadedFilePaths = new List<string>();
            var wwwrootPath = Path.Combine(_uploadPath, request.UserId.ToString());

            if (!Directory.Exists(_uploadPath)) { Directory.CreateDirectory(wwwrootPath); }

            foreach (var file in request.Files) {
                var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                var filePath = Path.Combine(wwwrootPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                }

                var relativePath = Path.Combine($"{request.BaseUrl}/uploads", request.UserId.ToString(), fileName).Replace('\\', '/');
                uploadedFilePaths.Add(relativePath);
            }

            return uploadedFilePaths;
        }
    }
}
