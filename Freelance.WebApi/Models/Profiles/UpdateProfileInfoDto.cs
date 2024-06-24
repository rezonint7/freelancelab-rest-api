using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile;
using Freelance.Domain;

namespace Freelance.WebApi.Models.Profiles
{
    public class UpdateProfileInfoDto : IMapWith<UpdateUserProfileCommand> {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? About { get; set; }

        public IFormFile? AvatarFile { get; set; }
        public IFormFile? HeaderFile { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateProfileInfoDto, UpdateUserProfileCommand>()
                .ForMember(cmd => cmd.UserId, opt => opt.MapFrom(dto => dto.UserId))
                .ForMember(cmd => cmd.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(cmd => cmd.LastName, opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(cmd => cmd.MiddleName, opt => opt.MapFrom(dto => dto.MiddleName))
                .ForMember(cmd => cmd.Birthday, opt => opt.MapFrom(dto => dto.Birthday))
                .ForMember(cmd => cmd.About, opt => opt.MapFrom(dto => dto.About))
                .ForMember(cmd => cmd.AvatarFile, opt => opt.MapFrom(dto => dto.AvatarFile))
                .ForMember(cmd => cmd.HeaderFile, opt => opt.MapFrom(dto => dto.HeaderFile));
        }
    }

}
