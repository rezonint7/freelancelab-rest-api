using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderList {
    public class GetOrderListQuery: IRequest<OrderListViewModel> {
        public string? Search { get; set; } = string.Empty;
        public string? Categories { get; set; } = "-1";
        public string? AdditionalCategories { get; set; } = "null";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
