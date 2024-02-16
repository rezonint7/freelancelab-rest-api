using AutoMapper;
using Freelance.Application.Admin.Commands.UpdateAdmin;
using Freelance.Application.Common.Mapping;

namespace Freelance.WebApi.Models {
    public class UpdateAdminDto: IMapWith<UpdateAdminCommand> {
        public Guid AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateAdminDto, UpdateAdminCommand>()
                .ForMember(adminUpdate => adminUpdate.AdminId,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.AdminId))
                .ForMember(adminUpdate => adminUpdate.FirstName,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.FirstName))
                .ForMember(adminUpdate => adminUpdate.LastName,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.LastName))
                .ForMember(adminUpdate => adminUpdate.MiddleName,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.MiddleName))
                .ForMember(adminUpdate => adminUpdate.Birthday,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.Birthday))
                .ForMember(adminUpdate => adminUpdate.Email,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.Email))
                .ForMember(adminUpdate => adminUpdate.Phone,
                    opt => opt.MapFrom(adminUpdate => adminUpdate.Phone));
        }
    }
}
