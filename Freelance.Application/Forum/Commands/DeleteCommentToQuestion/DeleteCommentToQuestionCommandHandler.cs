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

namespace Freelance.Application.Forum.Commands.DeleteCommentToQuestion {
    internal class DeleteCommentToQuestionCommandHandler : IRequestHandler<DeleteCommentToQuestionCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public DeleteCommentToQuestionCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteCommentToQuestionCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var comment = await _freelanceDBContext.CommentsToQuestions.FirstOrDefaultAsync(comment => comment.Id == request.CommentId, cancellationToken);
            
            if (comment == null) { throw new NotFoundException(nameof(CommentToQuestionForum), request.CommentId); }
            if (comment.User != user) { throw new NotFoundException(nameof(CommentToQuestionForum), request.CommentId); }

            _freelanceDBContext.CommentsToQuestions.Remove(comment);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
