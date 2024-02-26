using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderList {
    internal class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(
            IFreelanceDBContext freelanceDBContext,
            IMapper mapper)
            => (_freelanceDBContext, _mapper) = (freelanceDBContext, mapper);

        public async Task<OrderListViewModel> Handle(GetOrderListQuery request, CancellationToken cancellationToken) {
            IQueryable<Order> ordersQuery = _freelanceDBContext.Orders;
            if (!string.IsNullOrEmpty(request.Search)) {
                ordersQuery = ordersQuery.Where(order => order.Title.ToLower().Contains(request.Search.ToLower()));
            }
            if(request.Category != -1) {
                ordersQuery = ordersQuery.Where(order => order.CategoryId == request.Category);
            }

            int totalItems = await ordersQuery.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            ordersQuery = ordersQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var orders = await ordersQuery
                .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (_freelanceDBContext.Currencies.Count() == 0 && 
                _freelanceDBContext.Categories.Count() == 0 &&
                _freelanceDBContext.WorkExperience.Count() == 0) {

                await _freelanceDBContext.Currencies.AddRangeAsync(
                    new Domain.Currency { Name = "Рубли", Code = "RUB"}    
                );
                await _freelanceDBContext.OrderStatuses.AddRangeAsync(
                   new Domain.Status { Id = "archived", Name = "В архиве" },
                   new Domain.Status { Id = "completed", Name = "Завершен" },
                   new Domain.Status { Id = "open", Name = "Открыт" },
                   new Domain.Status { Id = "canceled", Name = "Отменен" }
               );
                await _freelanceDBContext.Categories.AddRangeAsync(
                    new Domain.Category { Name = "Разработка" },
                    new Domain.Category { Name = "Дизайн" },
                    new Domain.Category { Name = "Маркетинг" }
                );
                await _freelanceDBContext.WorkExperience.AddRangeAsync(
                    new Domain.WorkExperience { Name = "нет опыта" },
                    new Domain.WorkExperience { Name = "менее года" },
                    new Domain.WorkExperience { Name = "более года" },
                    new Domain.WorkExperience { Name = "более двух лет" },
                    new Domain.WorkExperience { Name = "более трех лет" }
                );
                await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            }
            return new OrderListViewModel {
                Orders = orders,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
