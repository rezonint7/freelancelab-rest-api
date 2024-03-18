using FluentValidation;

namespace Freelance.Application.Forum.Commands.DeleteCommentToQuestion {
    internal class DeleteCommentToQuestionCommandValidator: AbstractValidator<DeleteCommentToQuestionCommand> {
        public DeleteCommentToQuestionCommandValidator() {
            RuleFor(comment => comment.UserId).NotEqual(Guid.Empty);
            RuleFor(comment => comment.CommentId).NotEmpty().InclusiveBetween(0, int.MaxValue);
        }
    }
}
