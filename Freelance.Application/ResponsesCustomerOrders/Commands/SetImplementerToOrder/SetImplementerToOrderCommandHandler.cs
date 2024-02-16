using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.ResponsesCustomerOrders.Commands.SetImplementerToOrder {
    internal class SetImplementerToOrderCommandHandler : IRequestHandler<SetImplementerToOrderCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        public SetImplementerToOrderCommandHandler(IFreelanceDBContext freelanceDBContext) {
            _freelanceDBContext = freelanceDBContext;
        }

        public async Task<Unit> Handle(SetImplementerToOrderCommand request, CancellationToken cancellationToken) {
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId ==  request.OrderId, cancellationToken);
            var implementer = await _freelanceDBContext.Implementers.FirstOrDefaultAsync(impl => impl.UserId ==  request.ImplementerId, cancellationToken);
            if(order == null || order.CustomerId != request.CustomerId) {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }
            if (implementer == null) {
                throw new NotFoundException(nameof(Implementer), request.ImplementerId);
            }

            order.ImplementerId = request.ImplementerId;
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
