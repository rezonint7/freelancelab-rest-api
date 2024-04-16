using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.Application.Orders.Commands.SetImplementerToOrder
{
    internal class SetImplementerToOrderCommandHandler : IRequestHandler<SetImplementerToOrderCommand, Unit>
    {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IChatService _chatService;
        public SetImplementerToOrderCommandHandler(IFreelanceDBContext freelanceDBContext, IChatService chatService)
        {
            _freelanceDBContext = freelanceDBContext;
            _chatService = chatService;
        }

        public async Task<Unit> Handle(SetImplementerToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId == request.OrderId, cancellationToken);
            var implementer = await _freelanceDBContext.Implementers.FirstOrDefaultAsync(impl => impl.UserId == request.ImplementerId, cancellationToken);
            var status = await _freelanceDBContext.Statuses.FirstOrDefaultAsync(status => status.Id == "in_progress");
            if (order == null || order.CustomerId != request.CustomerId) { throw new NotFoundException(nameof(Order), request.OrderId); }
            if (implementer == null) { throw new NotFoundException(nameof(Implementer), request.ImplementerId); }
            if (status == null) { throw new NotFoundException(nameof(Status), "in_progress"); }

            order.ImplementerId = request.ImplementerId;
            order.Status = status;
            await _chatService.CreateNewChatAsync(request.ImplementerId, order.CustomerId, order.OrderId, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
