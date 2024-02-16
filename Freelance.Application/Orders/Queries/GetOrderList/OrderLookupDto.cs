using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderList {
    public class OrderLookupDto : IMapWith<Order> {
        public Guid OrderId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public CustomerLookupDto Customer { get; set; }
        public decimal? ProjectFee { get; set; }
        public string Status { get; set; }
        public bool IsUrgent { get; set; }
        public Currency? Currency { get; set; }
        public Guid? ImplementerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(order => order.OrderId, 
                    opt => opt.MapFrom(order => order.OrderId))
                .ForMember(order => order.Title,
                    opt => opt.MapFrom(order => order.Title))
                .ForMember(order => order.Tags,
                    opt => opt.MapFrom(order => order.Tags))
                .ForMember(order => order.ProjectFee,
                    opt => opt.MapFrom(order => order.ProjectFee))
                .ForMember(order => order.Status,
                    opt => opt.MapFrom(order => order.StatusId))
                .ForMember(order => order.IsUrgent,
                    opt => opt.MapFrom(order => order.IsUrgent))
                .ForMember(order => order.Currency,
                    opt => opt.MapFrom(order => order.Currency))
                .ForMember(order => order.Customer,
                    opt => opt.MapFrom(order => order.Customer))
                .ForMember(order => order.CreatedAt,
                    opt => opt.MapFrom(order => order.CreatedAt))
                .ForMember(order => order.ImplementerId,
                    opt => opt.MapFrom(order => order.ImplementerId));
        }
    }
}
