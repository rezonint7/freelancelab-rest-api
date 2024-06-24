using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Orders.Commands.CreateOrder;

namespace Freelance.WebApi.Models.Orders {
	public class CreateFeedbackDto: IMapWith<CreateFeedbackCommand> {
		public Guid OrderId { get; set; }
		public string FeedbackMessage { get; set; }
		public int FeedbackRating { get; set; }

		public void Mapping(Profile profile) {
			profile.CreateMap<CreateFeedbackDto, CreateFeedbackCommand>()
				.ForMember(createFeedbackCommand => createFeedbackCommand.OrderId,
					opt => opt.MapFrom(createFeedbackCommand => createFeedbackCommand.OrderId))
				.ForMember(createFeedbackCommand => createFeedbackCommand.FeedbackMessage,
					opt => opt.MapFrom(createFeedbackCommand => createFeedbackCommand.FeedbackMessage))
				.ForMember(createFeedbackCommand => createFeedbackCommand.FeedbackRating,
					opt => opt.MapFrom(createFeedbackCommand => createFeedbackCommand.FeedbackRating));
		}
	}
}
