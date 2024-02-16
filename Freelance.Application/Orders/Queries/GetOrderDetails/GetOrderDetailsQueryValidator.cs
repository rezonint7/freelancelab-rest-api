using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderDetails {
    internal class GetOrderDetailsQueryValidator: AbstractValidator<GetOrderDetailsQuery> {
        public GetOrderDetailsQueryValidator() => RuleFor(getOrderDetailsQuery => getOrderDetailsQuery.OrderId).NotEqual(Guid.Empty);
    }
}
