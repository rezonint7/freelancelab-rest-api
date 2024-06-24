using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem {
	internal class CreateNewPortfolioItemCommandHandler : IRequestHandler<CreateNewPortfolioItemCommand, int> {
		private readonly IFreelanceDBContext _freelanceDBContext;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public CreateNewPortfolioItemCommandHandler(IFreelanceDBContext freelanceDBContext, IWebHostEnvironment webHostEnvironment) {
			_freelanceDBContext = freelanceDBContext;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<int> Handle(CreateNewPortfolioItemCommand request, CancellationToken cancellationToken) {
			var impl = await _freelanceDBContext.Implementers.FindAsync(request.ImplementerId, cancellationToken);
			var category = await _freelanceDBContext.Categories.FindAsync(request.CategoryId, cancellationToken);
			if (impl == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }
			if (category == null) { throw new NotFoundException(nameof(Category), request.CategoryId); }

			var portfolioItem = new PortfolioItem {
				Title = request.Title,
				Description = request.Description,
				Implementer = impl,
				Category = category,
				CreatedAt = DateTime.Now,
				PhotoPath = ""
			};

			if (request.PhotoFile != null) {
				var portfolioDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "portfolio", request.ImplementerId.ToString());
				if (!Directory.Exists(portfolioDirectory)) {
					Directory.CreateDirectory(portfolioDirectory);
				}
				var photoPath = Path.Combine(portfolioDirectory, request.PhotoFile.FileName);
				using (var stream = new FileStream(photoPath, FileMode.Create)) {
					await request.PhotoFile.CopyToAsync(stream);
				}
				portfolioItem.PhotoPath = Path.Combine("uploads", "portfolio", request.ImplementerId.ToString(), request.PhotoFile.FileName).Replace('\\', '/');
			}

			await _freelanceDBContext.PortfolioItems.AddAsync(portfolioItem, cancellationToken);
			await _freelanceDBContext.SaveChangesAsync(cancellationToken);

			return portfolioItem.Id;
		}
	}
}
