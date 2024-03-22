using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;

namespace Freelance.WebApi.Models.Portfolio
{
    public class CreatePortfolioItemDto : IMapWith<CreateNewPortfolioItemCommand>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePortfolioItemDto, CreateNewPortfolioItemCommand>()
                .ForMember(orderCommand => orderCommand.Title,
                    opt => opt.MapFrom(orderDto => orderDto.Title))
                .ForMember(orderCommand => orderCommand.Description,
                    opt => opt.MapFrom(orderDto => orderDto.Description));
        }
    }
}
