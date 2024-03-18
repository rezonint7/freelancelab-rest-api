using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.UpdateAnswerToComment {
    internal class UpdateAnswerToCommentCommandValidator: AbstractValidator<UpdateAnswerToCommentCommand> {
        public UpdateAnswerToCommentCommandValidator() {
            RuleFor(updateAnswer => updateAnswer.UserId).NotEqual(Guid.Empty);
            RuleFor(updateAnswer => updateAnswer.AnswerToCommentId).NotEmpty().InclusiveBetween(0, int.MaxValue);
            RuleFor(updateAnswer => updateAnswer.AnswerMessage).NotEmpty().MinimumLength(10).MaximumLength(5000);
        }
    }
}
