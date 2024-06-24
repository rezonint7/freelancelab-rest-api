using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CreateOrder {
	internal class CreateFeedbackCommandValidator: AbstractValidator<CreateFeedbackCommand> {
        public CreateFeedbackCommandValidator()
        {
			RuleFor(createFeedbackCommand => createFeedbackCommand.UserId).NotEqual(Guid.Empty);
			RuleFor(createFeedbackCommand => createFeedbackCommand.OrderId).NotEqual(Guid.Empty);
			RuleFor(createFeedbackCommand => createFeedbackCommand.FeedbackMessage).NotEmpty()
				.MinimumLength(10)
				.WithName("Отзыв")
				.MaximumLength(800);
			RuleFor(createFeedbackCommand => createFeedbackCommand.FeedbackRating).NotEmpty()
				.WithName("Оценка отзыва").InclusiveBetween(1, 5);
		}
    }
}
