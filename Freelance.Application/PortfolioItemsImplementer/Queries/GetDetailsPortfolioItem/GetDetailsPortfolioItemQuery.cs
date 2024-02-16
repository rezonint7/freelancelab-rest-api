using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.PortfolioItemsImplementer.Queries.GetDetailsPortfolioItem {
    public class GetDetailsPortfolioItemQuery: IRequest<PortfolioItemViewModel> {
        public int PortfolioItemId { get; set; }
        public Guid ImplementerId { get; set; }
    }
}
