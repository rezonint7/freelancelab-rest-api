using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;

namespace Freelance.Application.PortfolioItemsImplementer.Queries.GetDetailsPortfolioItem {
    public class PortfolioItemViewModel: IMapWith<PortfolioItem> {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Category { get; set; }

        public ImplementerPortfolioLookupDto Implementer { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<PortfolioItem, PortfolioItemViewModel>()
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.Title,
                    opt => opt.MapFrom(portfolio => portfolio.Title))
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.Description,
                    opt => opt.MapFrom(portfolio => portfolio.Description))
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.PhotoPath,
                    opt => opt.MapFrom(portfolio => portfolio.PhotoPath))
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.CreatedAt,
                    opt => opt.MapFrom(portfolio => portfolio.CreatedAt))
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.Category,
                    opt => opt.MapFrom(portfolio => portfolio.Category.Name))
                .ForMember(portfolioItemViewModel => portfolioItemViewModel.Implementer,
                    opt => opt.MapFrom(portfolio => portfolio.Implementer));
        }
    }
}
