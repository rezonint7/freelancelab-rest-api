using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CreateOrder {
	public class CreateFeedbackCommand: IRequest<Unit> {
		public Guid UserId { get; set; }
		public Guid OrderId { get; set; }
		public string FeedbackMessage { get; set; }
		public int FeedbackRating { get; set; }
	}
}
