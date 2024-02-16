using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList {
    public class ImplementerLookupDto : IMapWith<Implementer> {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialization { get; set; }
        public string? Skills { get; set; }
        public double Rating { get; set; }

        public string AvatarProfilePath { get; set; }

        public string About { get; set; }

        public IList<PortfolioImplementerLookupDto> Portfolio { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Implementer, ImplementerLookupDto>()
                .ForMember(implementerLookupDto => implementerLookupDto.UserId,
                    opt => opt.MapFrom(implementer => implementer.UserId))
                .ForMember(implementerLookupDto => implementerLookupDto.UserName,
                    opt => opt.MapFrom(implementer => implementer.User.UserName))
                .ForMember(implementerLookupDto => implementerLookupDto.FirstName,
                    opt => opt.MapFrom(implementer => implementer.User.FirstName))
                .ForMember(implementerLookupDto => implementerLookupDto.LastName,
                    opt => opt.MapFrom(implementer => implementer.User.LastName))
                .ForMember(implementerLookupDto => implementerLookupDto.MiddleName,
                    opt => opt.MapFrom(implementer => implementer.User.MiddleName))
                .ForMember(implementerLookupDto => implementerLookupDto.AvatarProfilePath,
                    opt => opt.MapFrom(implementer => implementer.User.AvatarProfilePath))
                .ForMember(implementerLookupDto => implementerLookupDto.Rating,
                    opt => opt.MapFrom(implementer => implementer.User.Rating))
                .ForMember(implementerLookupDto => implementerLookupDto.About,
                    opt => opt.MapFrom(implementer => implementer.User.About))
                .ForMember(implementerLookupDto => implementerLookupDto.Specialization,
                    opt => opt.MapFrom(implementer => implementer.Specialization))
                .ForMember(implementerLookupDto => implementerLookupDto.Skills,
                    opt => opt.MapFrom(implementer => implementer.Skills))
                .ForMember(implementerLookupDto => implementerLookupDto.Portfolio,
                    opt => opt.MapFrom(implementer => implementer.Portfolio));
        }
    }
}
