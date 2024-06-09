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

namespace Freelance.Application.Orders.Commands.CreateResponseImplementer
{
    internal class CreateNewResponseCommandHandler : IRequestHandler<CreateNewResponseCommand, Unit>
    {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public CreateNewResponseCommandHandler(IFreelanceDBContext freelanceDBContext)
        {
            _freelanceDBContext = freelanceDBContext;
        }

        public async Task<Unit> Handle(CreateNewResponseCommand request, CancellationToken cancellationToken)
        {
            var order = await _freelanceDBContext.Orders.FindAsync(request.OrderId, cancellationToken);
			if (order == null) { throw new NotFoundException(nameof(Order), request.OrderId); }
			var implementer = await _freelanceDBContext.Implementers
                .Include(i => i.User)
                .FirstOrDefaultAsync(impl => impl.UserId == request.ImplementerId, cancellationToken);
			if (implementer == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }

			var response = new ResponseImplementer
            {
                Order = order,
                Implementer = implementer,
                ResponseMessage = request.ResponseMessage,
                CreatedAt = DateTime.Now,
            };
			if (order.Responses.Contains(response)) { throw new ItemAlreadyExistsException(nameof(ResponseImplementer), implementer.User.UserName); }

			order.Responses.Add(response);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
