using AutoMapper;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.Application.Orders.Queries.GetOrderDetails {
    internal class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(
            IFreelanceDBContext freelanceDBContext, 
            IMapper mapper) 
            => (_freelanceDBContext, _mapper) = (freelanceDBContext, mapper);
        public async Task<OrderDetailsViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken) {
            var order = await _freelanceDBContext.Orders
                .Include(i => i.Currency)
                .Include(i => i.Customer)
                .Include(i => i.Customer)
                .ThenInclude(i => i.User)
                .Include(i => i.Responses)
                .ThenInclude(impl => impl.Implementer)
                .ThenInclude(i => i.User)
                .Include(i => i.Responses)
                .ThenInclude(impl => impl.Implementer)
                .ThenInclude(impl => impl.WorkExperience)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(order => order.OrderId == request.OrderId, cancellationToken);
            if (order == null) { throw new NotFoundException(nameof(Order), request.OrderId); }
            
            if (request.UserId != order.CustomerId) {
				var userResponse = order.Responses.FirstOrDefault(r => r.ImplementerId == request.UserId);
				if (userResponse != null) {
                    order.Responses.Clear();
					order.Responses.Add(userResponse);
				}
				else {
					order.Responses.Clear();
				}
			}

            return _mapper.Map<OrderDetailsViewModel>(order);
        }

	}
}
