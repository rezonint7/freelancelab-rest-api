using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewQuestionForum {
    internal class CreateNewQuestionForumCommandValidator : AbstractValidator<CreateNewQuestionForumCommand>{
        public CreateNewQuestionForumCommandValidator() {
            RuleFor(newQuestion => newQuestion.UserId).NotEqual(Guid.Empty);
            RuleFor(newQuestion => newQuestion.Title).NotEmpty().MinimumLength(10).MaximumLength(180);
            RuleFor(newQuestion => newQuestion.Content).NotEmpty().MinimumLength(10).MaximumLength(5000);
            RuleFor(newQuestion => newQuestion.Tags).MaximumLength(500);
        }
    }
}
