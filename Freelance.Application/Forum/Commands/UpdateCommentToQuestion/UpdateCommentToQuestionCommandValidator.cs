using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.UpdateCommentToQuestion {
    internal class UpdateCommentToQuestionCommandValidator: AbstractValidator<UpdateCommentToQuestionCommand> {
        public UpdateCommentToQuestionCommandValidator() {
            RuleFor(updateComment => updateComment.UserId).NotEqual(Guid.Empty);
            RuleFor(updateComment => updateComment.CommentId).NotEmpty().InclusiveBetween(0, int.MaxValue);
            RuleFor(updateComment => updateComment.CommentMessage).NotEmpty().MinimumLength(10).MaximumLength(5000);
        }
    }
}
