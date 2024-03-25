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

namespace Freelance.Application.Orders.Commands.CompleteOrder
{
    internal class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, Unit>
    {
        private readonly IFreelanceDBContext _freelanceDBContext;
        public CompleteOrderCommandHandler(IFreelanceDBContext freelanceDBContext)
        {
            _freelanceDBContext = freelanceDBContext;
        }
        public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId == request.OrderId, cancellationToken);
            if (order == null || order.CustomerId != request.CustomerId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }
            var status = await _freelanceDBContext.Statuses.FirstOrDefaultAsync(status => status.Name == "completed");
            order.Status = status;
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
