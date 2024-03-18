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

namespace Freelance.Application.Forum.Commands.UpdateAnswerToComment {
    internal class UpdateAnswerToCommentCommandHandler : IRequestHandler<UpdateAnswerToCommentCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public UpdateAnswerToCommentCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateAnswerToCommentCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var answer = await _freelanceDBContext.AnswerToComments.FirstOrDefaultAsync(answer => answer.Id == request.AnswerToCommentId, cancellationToken);

            if (answer == null) { throw new NotFoundException(nameof(AnswerToComment), request.AnswerToCommentId); }
            if (answer.User != user) { throw new NotFoundException(nameof(AnswerToComment), request.AnswerToCommentId); }

            answer.AnswerMessage = request.AnswerMessage;
            answer.UpdatedAt = DateTime.Now;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
