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

namespace Freelance.Application.Forum.Commands.UpdateQuestionForum {
    internal class UpdateQuestionForumCommandHandler : IRequestHandler<UpdateQuestionForumCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public UpdateQuestionForumCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateQuestionForumCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var question = await _freelanceDBContext.QuestionsForum.FirstOrDefaultAsync(question => question.Id == request.QuestionId, cancellationToken);
            if(question == null) { throw new NotFoundException(nameof(QuestionForum), request.QuestionId); }
            if(question.User != user) { throw new NotFoundException(nameof(QuestionForum), request.QuestionId); }

            question.Title = request.Title;
            question.Content = request.Content;
            question.Tags = request.Tags;
            question.UpdatedAt = DateTime.Now;

            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
