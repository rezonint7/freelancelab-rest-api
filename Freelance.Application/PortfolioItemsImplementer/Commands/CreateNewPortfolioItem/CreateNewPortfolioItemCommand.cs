using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem {
	public class CreateNewPortfolioItemCommand : IRequest<int> {
		public Guid ImplementerId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public IFormFile? PhotoFile { get; set; }
		public int CategoryId { get; set; }
	}
}
