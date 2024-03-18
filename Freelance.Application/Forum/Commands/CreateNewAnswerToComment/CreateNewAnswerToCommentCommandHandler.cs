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

namespace Freelance.Application.Forum.Commands.CreateNewAnswerToComment {
    internal class CreateNewAnswerToCommentCommandHandler : IRequestHandler<CreateNewAnswerToCommentCommand, int> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public CreateNewAnswerToCommentCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<int> Handle(CreateNewAnswerToCommentCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var comment = await _freelanceDBContext.CommentsToQuestions.FirstOrDefaultAsync(comment => comment.Id == request.CommentToQuestionForumId, cancellationToken);
            
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            if (comment == null) { throw new NotFoundException(nameof(CommentToQuestionForum), request.CommentToQuestionForumId); }

            var answer = new AnswerToComment {
                AnswerMessage = request.AnswerMessage,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                User = user,
                CommentToQuestionForum = comment,
            };

            await _freelanceDBContext.AnswerToComments.AddAsync(answer, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return answer.Id;
        }
    }
}
