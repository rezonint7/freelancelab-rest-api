using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Orders.Commands.CreateOrder;
using Freelance.Application.Orders.Commands.CreateResponseImplementer;

namespace Freelance.WebApi.Models.Orders {
    public class CreateResponseDto: IMapWith<CreateNewResponseCommand> {
        public string ResponseMessage { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateResponseDto, CreateNewResponseCommand>()
                .ForMember(responseCommand => responseCommand.ResponseMessage,
                    opt => opt.MapFrom(responseDto => responseDto.ResponseMessage));
        }
    }
}
