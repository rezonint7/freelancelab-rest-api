using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Orders.Commands.SetImplementerToOrder;
using Freelance.Application.Orders.Commands.UpdateOrder;

namespace Freelance.WebApi.Models.Orders
{
    public class SetImplementerToOrderDto : IMapWith<SetImplementerToOrderCommand>
    {
        public Guid ImplementerId { get; set; }
        public Guid OrderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SetImplementerToOrderDto, SetImplementerToOrderCommand>()
                .ForMember(setImpl => setImpl.ImplementerId,
                    opt => opt.MapFrom(setImpl => setImpl.ImplementerId))
                .ForMember(setImpl => setImpl.OrderId,
                    opt => opt.MapFrom(setImpl => setImpl.OrderId));
        }
    }
}
