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

namespace Freelance.Application.Forum.Commands.DeleteQuestionForum {
    internal class DeleteQuestionForumCommandHandler : IRequestHandler<DeleteQuestionForumCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public DeleteQuestionForumCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteQuestionForumCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var question = await _freelanceDBContext.QuestionsForum.FirstOrDefaultAsync(question => question.Id == request.QuestionId, cancellationToken);
            if (question == null) { throw new NotFoundException(nameof(QuestionForum), request.QuestionId); }
            if (question.User != user) { throw new NotFoundException(nameof(QuestionForum), request.QuestionId); }

            _freelanceDBContext.QuestionsForum.Remove(question);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
