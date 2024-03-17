using Freelance.Application.Common.Mapping;
using Freelance.Application.Auth.Commands.RegisterNewUser;
using AutoMapper;

namespace Freelance.WebApi.Models {
    public class RegisterNewUserDto: IMapWith<RegisterNewUserCommand> {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<RegisterNewUserDto, RegisterNewUserCommand>()
                .ForMember(userCommand => userCommand.Login,
                    opt => opt.MapFrom(userCommand => userCommand.Login))
                .ForMember(userCommand => userCommand.Password,
                    opt => opt.MapFrom(userCommand => userCommand.Password))
                .ForMember(userCommand => userCommand.Role,
                    opt => opt.MapFrom(userCommand => userCommand.Role))
                .ForMember(userCommand => userCommand.FirstName,
                    opt => opt.MapFrom(userCommand => userCommand.FirstName))
                .ForMember(userCommand => userCommand.LastName,
                    opt => opt.MapFrom(userCommand => userCommand.LastName))
                .ForMember(userCommand => userCommand.MiddleName,
                    opt => opt.MapFrom(userCommand => userCommand.MiddleName))
                .ForMember(userCommand => userCommand.Email,
                    opt => opt.MapFrom(userCommand => userCommand.Email));
        }
    }
}
