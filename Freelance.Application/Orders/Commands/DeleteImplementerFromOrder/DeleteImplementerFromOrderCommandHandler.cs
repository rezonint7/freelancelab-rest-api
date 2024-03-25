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

namespace Freelance.Application.Orders.Commands.DeleteImplementerFromOrder
{
    internal class DeleteImplementerFromOrderCommandHandler : IRequestHandler<DeleteImplementerFromOrderCommand, Unit>
    {
        private readonly IFreelanceDBContext _freelanceDBContext;
        public DeleteImplementerFromOrderCommandHandler(IFreelanceDBContext freelanceDBContext)
        {
            _freelanceDBContext = freelanceDBContext;
        }
        public async Task<Unit> Handle(DeleteImplementerFromOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId == request.OrderId, cancellationToken);
            var implementer = await _freelanceDBContext.Implementers.FirstOrDefaultAsync(impl => impl.UserId == request.ImplementerId, cancellationToken);
            if (order == null || order.CustomerId != request.CustomerId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }
            if (order.ImplementerId != request.ImplementerId || implementer == null)
            {
                throw new NotFoundException(nameof(Implementer), request.ImplementerId);
            }

            order.ImplementerId = Guid.Empty;
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
