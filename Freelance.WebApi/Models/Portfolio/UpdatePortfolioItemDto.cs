using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.PortfolioItemsImplementer.Commands.CreateNewPortfolioItem;
using Freelance.Application.PortfolioItemsImplementer.Commands.UpdatePortfolioItem;

namespace Freelance.WebApi.Models.Portfolio
{
    public class UpdatePortfolioItemDto : IMapWith<UpdatePortfolioItemCommand>
    {
        public int PortfolioItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public string CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePortfolioItemDto, UpdatePortfolioItemCommand>()
                 .ForMember(portfolioUpdateCommand => portfolioUpdateCommand.PortfolioItemId,
                    opt => opt.MapFrom(orderDto => orderDto.PortfolioItemId))
                .ForMember(portfolioUpdateCommand => portfolioUpdateCommand.Title,
                    opt => opt.MapFrom(orderDto => orderDto.Title))
                .ForMember(portfolioUpdateCommand => portfolioUpdateCommand.Description,
                    opt => opt.MapFrom(orderDto => orderDto.Description))
                .ForMember(portfolioUpdateCommand => portfolioUpdateCommand.PhotoPath,
                    opt => opt.MapFrom(orderDto => orderDto.PhotoPath))
                .ForMember(portfolioUpdateCommand => portfolioUpdateCommand.CategoryId,
                    opt => opt.MapFrom(orderDto => orderDto.CategoryId));
        }
    }
}
