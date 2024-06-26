﻿using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Orders.Commands.UpdateOrder;

namespace Freelance.WebApi.Models.Orders
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        public Guid OrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ProjectFee { get; set; }
        public int CurrencyId { get; set; }
        public int CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(orderCommand => orderCommand.OrderId,
                    opt => opt.MapFrom(orderDto => orderDto.OrderId))
                .ForMember(orderCommand => orderCommand.Title,
                    opt => opt.MapFrom(orderDto => orderDto.Title))
                .ForMember(orderCommand => orderCommand.Description,
                    opt => opt.MapFrom(orderDto => orderDto.Description))
                .ForMember(orderCommand => orderCommand.ProjectFee,
                    opt => opt.MapFrom(orderDto => orderDto.ProjectFee))
                .ForMember(orderCommand => orderCommand.CurrencyId,
                    opt => opt.MapFrom(orderDto => orderDto.CurrencyId))
                .ForMember(orderCommand => orderCommand.CategoryId,
                    opt => opt.MapFrom(orderDto => orderDto.CategoryId));
        }
    }
}
