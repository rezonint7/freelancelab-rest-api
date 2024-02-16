using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.UpdateQuestionForum {
    internal class UpdateQuestionForumCommandValidator : AbstractValidator<UpdateQuestionForumCommand> {
        public UpdateQuestionForumCommandValidator() {
            RuleFor(newQuestion => newQuestion.UserId).NotEqual(Guid.Empty);
            RuleFor(newQuestion => newQuestion.QuestionId).GreaterThan(0);
            RuleFor(newQuestion => newQuestion.Title).NotEmpty().MinimumLength(10).MaximumLength(180);
            RuleFor(newQuestion => newQuestion.Content).NotEmpty().MinimumLength(10).MaximumLength(5000);
            RuleFor(newQuestion => newQuestion.Tags).MaximumLength(500);
        }
    }
}
