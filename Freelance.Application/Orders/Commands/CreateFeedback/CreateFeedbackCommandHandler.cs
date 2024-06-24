using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CreateOrder {
	internal class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Unit> {
		private readonly IFreelanceDBContext _freelanceDBContext;
		private readonly IUserService _userService;

        public CreateFeedbackCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
			_freelanceDBContext = freelanceDBContext;
			_userService = userService;	
        }

        public async Task<Unit> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken) {
			var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
			if(user == null) {
				throw new NotFoundException(nameof(ApplicationUser), request.UserId.ToString());
			}

			var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(i => i.OrderId == request.OrderId, cancellationToken);
			if (order == null) {
				throw new NotFoundException(nameof(Order), request.OrderId.ToString());
			}

			var feedback = new Feedback {
				Order = order,
				User = user,
				FeedbackMessage = request.FeedbackMessage,
				FeedbackRating = request.FeedbackRating,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = null,
			};

			await _freelanceDBContext.Feedbacks.AddAsync(feedback);
			await _freelanceDBContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;

		}
	}
}
