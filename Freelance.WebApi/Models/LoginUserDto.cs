using Freelance.Application.Common.Mapping;
using Freelance.Application.Auth.Queries.AuthenticateUser;
using AutoMapper;

namespace Freelance.WebApi.Models {
    public class LoginUserDto: IMapWith<AuthenticateUserQuery> {
        public string Login { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<LoginUserDto, AuthenticateUserQuery>()
                .ForMember(user => user.Login,
                    opt => opt.MapFrom(user => user.Login))
                .ForMember(user => user.Password,
                    opt => opt.MapFrom(user => user.Password));
        }
    }
}
