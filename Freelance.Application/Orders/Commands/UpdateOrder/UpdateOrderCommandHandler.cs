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

namespace Freelance.Application.Orders.Commands.UpdateOrder {
    internal class UpdateOrderCommandHandler: IRequestHandler<UpdateOrderCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public UpdateOrderCommandHandler(IFreelanceDBContext freelanceDBContext) =>
            _freelanceDBContext = freelanceDBContext;

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken) {
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId == request.OrderId, cancellationToken);
            var currency = await _freelanceDBContext.Currencies.FindAsync(request.CurrencyId, cancellationToken);
            var category = await _freelanceDBContext.Categories.FindAsync(request.CategoryId, cancellationToken);

            if (order == null) throw new NotFoundException(nameof(Order), request.OrderId);
            if (currency == null) { throw new NotFoundException(nameof(Currency), request.CurrencyId); }
            if (category == null) { throw new NotFoundException(nameof(Category), request.CategoryId); }
            if (order.CustomerId != request.CustomerId) { throw new NotFoundException(nameof(Order), request.OrderId); }
    
            order.Title = request.Title;
            order.Description = request.Description;
            order.UpdatedAt = DateTime.Now;
            order.ProjectFee = request.ProjectFee;
            order.Tags = request.Tags;
            order.IsUrgent = request.IsUrgent;
            order.Category = category;
            order.Currency = currency;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
