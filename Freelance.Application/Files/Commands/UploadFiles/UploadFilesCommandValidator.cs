using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Freelance.Application.Files.Commands.UploadFiles {
    internal class UploadFilesCommandValidator: AbstractValidator<UploadFilesCommand> {
        public UploadFilesCommandValidator() {
            RuleFor(files => files.UserId).NotEqual(Guid.Empty);
            RuleFor(files => files.BaseUrl).NotEmpty();
            RuleFor(files => files.Files)
            .NotEmpty().WithMessage("Необходимо загрузить хотя бы один файл.")
            .Must(files => files.Count <= 5).WithMessage("Максимальное количество файлов для загрузки - 5.")
            .ForEach(fileRule => {
                fileRule.Must(file => IsSupportedFileType(file)).WithMessage("Недопустимый формат файла. Поддерживаемые форматы: jpg, jpeg, png, pdf.");
                fileRule.Must(file => file.Length < 2 * 1024 * 1024).WithMessage("Размер файла не должен превышать 2 мб.");
            });
        }

        private bool IsSupportedFileType(IFormFile file) {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
