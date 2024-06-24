using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem {
	internal class CreateNewPortfolioItemCommandValidator : AbstractValidator<CreateNewPortfolioItemCommand> {
		public CreateNewPortfolioItemCommandValidator() {
			RuleFor(createNewPortfolio => createNewPortfolio.ImplementerId).NotEqual(Guid.Empty);
			RuleFor(createNewPortfolio => createNewPortfolio.Title).NotEmpty().MinimumLength(10).MaximumLength(250);
			RuleFor(createNewPortfolio => createNewPortfolio.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);
			RuleFor(createNewPortfolio => createNewPortfolio.PhotoFile)
				.Must(file => IsSupportedFileType(file)).WithMessage("Invalid file format. Supported formats: jpg, jpeg, png.")
				.Must(file => file.Length < 2 * 1024 * 1024).WithMessage("File size must not exceed 2MB.")
				.When(createNewPortfolio => createNewPortfolio.PhotoFile != null);
			RuleFor(createNewPortfolio => createNewPortfolio.CategoryId).NotEmpty();
		}

		private bool IsSupportedFileType(IFormFile file) {
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
			var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
			return allowedExtensions.Contains(fileExtension);
		}
	}
}
