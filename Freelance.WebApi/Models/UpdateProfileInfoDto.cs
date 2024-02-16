using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile;
using Freelance.Domain;

namespace Freelance.WebApi.Models {
    public class UpdateProfileInfoDto: IMapWith<UpdateUserProfileCommand> {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? About { get; set; }

        public string AvatarProfilePath { get; set; }
        public string HeaderProfilePath { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateProfileInfoDto, UpdateUserProfileCommand>()
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.UserId,
                    opt => opt.MapFrom(implementerDto => implementerDto.UserId))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.FirstName,
                    opt => opt.MapFrom(implementerDto => implementerDto.FirstName))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.LastName,
                    opt => opt.MapFrom(implementerDto => implementerDto.LastName))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.MiddleName,
                    opt => opt.MapFrom(implementerDto => implementerDto.MiddleName))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.Birthday,
                    opt => opt.MapFrom(implementerDto => implementerDto.Birthday))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.AvatarProfilePath,
                    opt => opt.MapFrom(implementerDto => implementerDto.AvatarProfilePath))
                .ForMember(implementerUpdateCommand => implementerUpdateCommand.HeaderProfilePath,
                    opt => opt.MapFrom(implementerDto => implementerDto.HeaderProfilePath));
        }
    }
}
