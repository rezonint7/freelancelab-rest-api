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

namespace Freelance.Application.Orders.Commands.DeleteOrder {
    internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public DeleteOrderCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) {
            var order = await _freelanceDBContext.Orders.FindAsync(new object[] { request.OrderId }, cancellationToken);
            if(order == null) { throw new NotFoundException(nameof(Order), request.OrderId); }

            var user = await _userService.GetUserByIdAsync(request.CustomerId, cancellationToken);
            if(user == null) { throw new NotFoundException(nameof(ApplicationUser), request.CustomerId); }

            var roles = await _userService.GetUserRoleByIdAsync(request.CustomerId, cancellationToken);


            if (order == null) throw new NotFoundException(nameof(Order), request.OrderId);
            if (!roles.Contains("ADMIN")) {
                if (order.CustomerId != request.CustomerId) {
                    throw new NotFoundException(nameof(Order), request.OrderId);
                }
            }
            _freelanceDBContext.Orders.Remove(order);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
