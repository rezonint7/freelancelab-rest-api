using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderDetails
{
    public class OrderDetailsViewModel : IMapWith<Order>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? ProjectFee { get; set; }
        public string Status { get; set; }
        public bool IsUrgent { get; set; }
        public Currency? Currency { get; set; }
        public Category? Category { get; set; }
        public CustomerLookupDto Customer { get; set; }

        public Guid ImplementerId { get; set; }

        public IList<ResponseLookupDto> Responses { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Order, OrderDetailsViewModel>()
                .ForMember(orderViewModel => orderViewModel.Title,
                    opt => opt.MapFrom(order => order.Title))
                .ForMember(orderViewModel => orderViewModel.Description,
                    opt => opt.MapFrom(order => order.Description))
                .ForMember(orderViewModel => orderViewModel.CreatedAt,
                    opt => opt.MapFrom(order => order.CreatedAt))
                .ForMember(orderViewModel => orderViewModel.UpdatedAt,
                    opt => opt.MapFrom(order => order.UpdatedAt))
                .ForMember(orderViewModel => orderViewModel.ProjectFee,
                    opt => opt.MapFrom(order => order.ProjectFee))
                .ForMember(orderViewModel => orderViewModel.Status,
                    opt => opt.MapFrom(order => order.StatusId))
                .ForMember(orderViewModel => orderViewModel.Currency,
                    opt => opt.MapFrom(order => order.Currency))
                .ForMember(orderViewModel => orderViewModel.Category,
                    opt => opt.MapFrom(order => order.Category))
                .ForMember(orderViewModel => orderViewModel.Customer,
                    opt => opt.MapFrom(order => order.Customer))
                .ForMember(orderViewModel => orderViewModel.Responses,
                    opt => opt.MapFrom(order => order.Responses))
                .ForMember(orderViewModel => orderViewModel.ImplementerId,
                    opt => opt.MapFrom(order => order.ImplementerId));
        }
    }
}
