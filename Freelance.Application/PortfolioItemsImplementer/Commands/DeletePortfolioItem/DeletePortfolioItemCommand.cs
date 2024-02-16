using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Commands.DeletePortfolioItem {
    public class DeletePortfolioItemCommand: IRequest<Unit> {
        public Guid ImplementerId { get; set; }
        public int PortfolioItemId { get; set; }
    }
}
