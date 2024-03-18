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

namespace Freelance.Application.Forum.Commands.CreateNewCommentToQuestion {
    internal class CreateNewCommentToQuestionCommandHandler : IRequestHandler<CreateNewCommentToQuestionCommand, int> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public CreateNewCommentToQuestionCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<int> Handle(CreateNewCommentToQuestionCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var question = await _freelanceDBContext.QuestionsForum.FirstOrDefaultAsync(question => question.Id == request.QuestionForumId, cancellationToken);
            
            if (question == null) { throw new NotFoundException(nameof(QuestionForum), request.QuestionForumId); }
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            var comment = new CommentToQuestionForum {
                CommentMessage = request.CommentMessage,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                User = user,
                QuestionForum = question
            };

            await _freelanceDBContext.CommentsToQuestions.AddAsync(comment, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
