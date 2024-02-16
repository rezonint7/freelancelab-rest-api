using AutoMapper;
using Freelance.Application.Admin.Queries.GelAllAdmins;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GetDetailsAdmin {
    public class AdminDetailsViewModel : IMapWith<ApplicationUser> {
        public Guid AdminId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ApplicationUser, AdminDetailsViewModel>()
                .ForMember(admin => admin.AdminId,
                    opt => opt.MapFrom(admin => admin.Id))
                .ForMember(admin => admin.UserName,
                    opt => opt.MapFrom(admin => admin.UserName))
                .ForMember(admin => admin.FirstName,
                    opt => opt.MapFrom(admin => admin.FirstName))
                .ForMember(admin => admin.LastName,
                    opt => opt.MapFrom(admin => admin.LastName))
                .ForMember(admin => admin.MiddleName,
                    opt => opt.MapFrom(admin => admin.MiddleName))
                .ForMember(admin => admin.Email,
                    opt => opt.MapFrom(admin => admin.Email))
                .ForMember(admin => admin.Phone,
                    opt => opt.MapFrom(admin => admin.PhoneNumber));
        }
    }
}
