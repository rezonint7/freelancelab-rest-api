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

namespace Freelance.Application.Orders.Commands.DeleteResponseImplementer
{
    internal class DeleteResponseCommandHandler : IRequestHandler<DeleteResponseCommand, Unit>
    {
        private readonly IFreelanceDBContext _freelanceDBContext;

        public DeleteResponseCommandHandler(IFreelanceDBContext freelanceDBContext)
        {
            _freelanceDBContext = freelanceDBContext;
        }

        public async Task<Unit> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
        {
            var response = _freelanceDBContext.ResponsesImplementer
                .Include(i => i.Implementer)
                .Include(i => i.Order)
                .FirstOrDefault(response => response.Id == request.ResponseId);
            if (response == null) { throw new NotFoundException(nameof(ResponseImplementer), request.ResponseId); }
            if (response.Implementer.UserId != request.ImplementerId) { throw new NotFoundException(nameof(ResponseImplementer), request.ResponseId); }
            response.Order.Responses.Remove(response);
            _freelanceDBContext.ResponsesImplementer.Remove(response);

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
