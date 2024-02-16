using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GetAllUsers
{
    public class UserLookupDto : IMapWith<ApplicationUser>
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string AvatarProfilePath { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserLookupDto>()
                .ForMember(admin => admin.UserId,
                    opt => opt.MapFrom(admin => admin.Id))
                .ForMember(admin => admin.Login,
                    opt => opt.MapFrom(admin => admin.UserName))
                .ForMember(admin => admin.FirstName,
                    opt => opt.MapFrom(admin => admin.FirstName))
                .ForMember(admin => admin.LastName,
                    opt => opt.MapFrom(admin => admin.LastName))
                .ForMember(admin => admin.AvatarProfilePath,
                    opt => opt.MapFrom(admin => admin.AvatarProfilePath))
                .ForMember(admin => admin.MiddleName,
                    opt => opt.MapFrom(admin => admin.MiddleName))
                .ForMember(admin => admin.Email,
                    opt => opt.MapFrom(admin => admin.Email))
                .ForMember(admin => admin.Phone,
                    opt => opt.MapFrom(admin => admin.PhoneNumber));
        }
    }
}
