using FluentValidation;
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

namespace Freelance.Application.Orders.Commands.CreateOrder {
    internal class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, Guid> {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public CreateNewOrderCommandHandler(IFreelanceDBContext freelanceDBContext) => 
            _freelanceDBContext = freelanceDBContext;
        public async Task<Guid> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken) {
            var customer = await _freelanceDBContext.Customers.FindAsync(request.CustomerId, cancellationToken);
            var currency = await _freelanceDBContext.Currencies.FindAsync(request.CurrencyId, cancellationToken);
            var category = await _freelanceDBContext.Categories.FindAsync(request.CategoryId, cancellationToken);
            var status = await _freelanceDBContext.OrderStatuses.FirstOrDefaultAsync(status => status.Id == "open");

            var order = new Order {
                OrderId = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Status = status,
                ProjectFee = request.ProjectFee,
                Tags = request.Tags,
                IsUrgent= request.IsUrgent,
                Category = category,
                Currency = currency,
                Customer = customer,
            };
            order.ImplementerId = null;
            order.Implementer = null;

            customer.Orders.Add(order);
            await _freelanceDBContext.Orders.AddAsync(order, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return order.OrderId;
        }
    }
}
