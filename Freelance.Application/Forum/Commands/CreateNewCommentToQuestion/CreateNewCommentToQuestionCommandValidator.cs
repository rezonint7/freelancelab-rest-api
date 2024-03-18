using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewCommentToQuestion {
    internal class CreateNewCommentToQuestionCommandValidator: AbstractValidator<CreateNewCommentToQuestionCommand> {
        public CreateNewCommentToQuestionCommandValidator() {
            RuleFor(newComment => newComment.UserId).NotEqual(Guid.Empty);
            RuleFor(newComment => newComment.QuestionForumId).NotEmpty().InclusiveBetween(0, int.MaxValue);
            RuleFor(newComment => newComment.CommentMessage).NotEmpty().MinimumLength(10).MaximumLength(5000);
        }
    }
}
