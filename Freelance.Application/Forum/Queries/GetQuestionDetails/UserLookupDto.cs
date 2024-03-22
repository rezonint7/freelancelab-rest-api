using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    public class UserLookupDto : IMapWith<ApplicationUser> {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarProfilePath { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ApplicationUser, UserLookupDto>()
                .ForMember(user => user.UserName,
                    opt => opt.MapFrom(user => user.UserName))
                .ForMember(user => user.FirstName,
                    opt => opt.MapFrom(user => user.FirstName))
                .ForMember(user => user.LastName,
                    opt => opt.MapFrom(user => user.LastName))
                .ForMember(user => user.AvatarProfilePath,
                    opt => opt.MapFrom(user => user.AvatarProfilePath));
        }
    }
}
