using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo
{
    public class PortfolioImplementerLookupDto : IMapWith<PortfolioItem>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PortfolioItem, PortfolioImplementerLookupDto>()
                .ForMember(portfolio => portfolio.Id,
                    opt => opt.MapFrom(portfolio => portfolio.Id))
                .ForMember(portfolio => portfolio.Title,
                    opt => opt.MapFrom(portfolio => portfolio.Title))
                .ForMember(portfolio => portfolio.PhotoPath,
                    opt => opt.MapFrom(portfolio => portfolio.PhotoPath));
        }
    }
}
