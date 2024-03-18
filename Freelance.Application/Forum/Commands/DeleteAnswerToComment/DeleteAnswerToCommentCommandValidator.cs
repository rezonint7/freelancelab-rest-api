using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.DeleteAnswerToComment {
    internal class DeleteAnswerToCommentCommandValidator: AbstractValidator<DeleteAnswerToCommentCommand> {
        public DeleteAnswerToCommentCommandValidator() {
            RuleFor(comment => comment.UserId).NotEqual(Guid.Empty);
            RuleFor(comment => comment.AnswerId).NotEmpty().InclusiveBetween(0, int.MaxValue);
        }
    }
}
