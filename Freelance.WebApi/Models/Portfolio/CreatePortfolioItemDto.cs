using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;

namespace Freelance.WebApi.Models.Portfolio
{
	public class CreatePortfolioItemDto : IMapWith<CreateNewPortfolioItemCommand> {
		public string Title { get; set; }
		public string Description { get; set; }
		public IFormFile? PhotoFile { get; set; }
		public int CategoryId { get; set; }

		public void Mapping(Profile profile) {
			profile.CreateMap<CreatePortfolioItemDto, CreateNewPortfolioItemCommand>()
				.ForMember(cmd => cmd.Title, opt => opt.MapFrom(dto => dto.Title))
				.ForMember(cmd => cmd.Description, opt => opt.MapFrom(dto => dto.Description))
				.ForMember(cmd => cmd.PhotoFile, opt => opt.MapFrom(dto => dto.PhotoFile))
				.ForMember(cmd => cmd.CategoryId, opt => opt.MapFrom(dto => dto.CategoryId));
		}
	}
}
