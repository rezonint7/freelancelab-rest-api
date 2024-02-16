using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.ResponsesCustomerOrders.Commands.DeleteImplementerFromOrder;

namespace Freelance.WebApi.Models {
    public class DeleteImplementerFromOrderDto: IMapWith<DeleteImplementerFromOrderCommand> {
        public Guid ImplementerId { get; set; }
        public Guid OrderId { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<DeleteImplementerFromOrderDto, DeleteImplementerFromOrderCommand>()
                .ForMember(setImpl => setImpl.ImplementerId,
                    opt => opt.MapFrom(setImpl => setImpl.ImplementerId))
                .ForMember(setImpl => setImpl.OrderId,
                    opt => opt.MapFrom(setImpl => setImpl.OrderId));
        }
    }
}
