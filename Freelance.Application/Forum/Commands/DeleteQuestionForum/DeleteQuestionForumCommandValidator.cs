using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.DeleteQuestionForum {
    internal class DeleteQuestionForumCommandValidator : AbstractValidator<DeleteQuestionForumCommand> {
        public DeleteQuestionForumCommandValidator() {
            RuleFor(newQuestion => newQuestion.UserId).NotEqual(Guid.Empty);
            RuleFor(newQuestion => newQuestion.QuestionId).GreaterThan(0);
        }
    }
}
