using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Application.Orders.Queries.GetOrderList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo
{
    public class OrderResponseLookupDto : IMapWith<ResponseImplementer>
    {
        public int Id { get; set; }
        public OrderLookupDto Order { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public Guid ImplementerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResponseImplementer, OrderResponseLookupDto>()
                .ForMember(response => response.Id,
                    opt => opt.MapFrom(response => response.Id))
                .ForMember(response => response.Order,
                    opt => opt.MapFrom(response => response.Order))
                .ForMember(response => response.ResponseMessage,
                    opt => opt.MapFrom(response => response.ResponseMessage))
                .ForMember(response => response.CreatedAt,
                    opt => opt.MapFrom(response => response.CreatedAt))
                .ForMember(response => response.UpdatedAt,
                    opt => opt.MapFrom(response => response.UpdatedAt));
        }
    }
}
