using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.LikeToQuestion {
    internal class LikeToQuestionCommandValidator: AbstractValidator<LikeToQuestionCommand> {
        public LikeToQuestionCommandValidator() {
            RuleFor(newQuestion => newQuestion.UserId).NotEqual(Guid.Empty);
            RuleFor(newQuestion => newQuestion.QuestionId).NotEmpty().InclusiveBetween(0, int.MaxValue);
        }
    }
}
