using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewAnswerToComment {
    internal class CreateNewAnswerToCommentCommandValidator: AbstractValidator<CreateNewAnswerToCommentCommand> {
        public CreateNewAnswerToCommentCommandValidator() {
            RuleFor(newAnswer => newAnswer.UserId).NotEqual(Guid.Empty);
            RuleFor(newAnswer => newAnswer.CommentToQuestionForumId).NotEmpty().InclusiveBetween(0, int.MaxValue);
            RuleFor(newAnswer => newAnswer.AnswerMessage).NotEmpty().MinimumLength(10).MaximumLength(5000);
        }
    }
}
