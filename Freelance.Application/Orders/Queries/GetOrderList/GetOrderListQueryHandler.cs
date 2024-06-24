using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Auth.Commands.RegisterNewUser;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderList {
    internal class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetOrderListQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<OrderListViewModel> Handle(GetOrderListQuery request, CancellationToken cancellationToken) {
            IQueryable<Order> ordersQuery = _freelanceDBContext.Orders.Where(i=> i.StatusId == "open");
            if (!string.IsNullOrEmpty(request.Search)) {
                ordersQuery = ordersQuery.Where(order => order.Title.ToLower().Contains(request.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.Categories) && request.Categories != "-1") {
                var categoriesSplit = request.Categories.Split(',').Select(int.Parse).ToList();
                ordersQuery = ordersQuery.Where(order => categoriesSplit.Contains(order.CategoryId));
            }
            if (!string.IsNullOrEmpty(request.AdditionalCategories) && request.AdditionalCategories != "null") {
                var additionalCategoriesSplit = request.AdditionalCategories.Split(',').ToList();
                foreach(var category in additionalCategoriesSplit) {
                    switch(category.ToLower()) {
                        case "with_cost":
                            ordersQuery = ordersQuery.Where(order => order.ProjectFee > 0);
                            break;
                        case "with_reviews":
                            ordersQuery = ordersQuery.Where(order => order.Customer.User.Feedbacks.Any());
                            break;
                        case "only_urgent":
                            ordersQuery = ordersQuery.Where(order => order.IsUrgent == true);
                            break;
                        default: break;
                    }
                }
            }

            int totalItems = await ordersQuery.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            ordersQuery = ordersQuery
                .OrderByDescending(order => order.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var orders = await ordersQuery
                .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (_freelanceDBContext.Currencies.Count() == 0 && 
                _freelanceDBContext.Categories.Count() == 0 &&
                _freelanceDBContext.WorkExperience.Count() == 0 &&
                _freelanceDBContext.ReasonsToReport.Count() == 0 &&
                _roleManager.Roles.Count() == 0) {

                var roles = new string[] { "Customer", "Implementer", "Admin", "Owner", "Manager", "OAuth" };
                foreach (var role in roles) {
                    var roleExist = await _roleManager.RoleExistsAsync(role);
                    if (!roleExist) {
                        await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    }
                }

                await _freelanceDBContext.Currencies.AddRangeAsync(
                    new Domain.Currency { Name = "Рубли", Code = "RUB"}    
                );
                await _freelanceDBContext.Statuses.AddRangeAsync(
                   new Domain.Status { Id = "archived", Name = "В архиве" },
                   new Domain.Status { Id = "completed", Name = "Завершен" },
                   new Domain.Status { Id = "open", Name = "Открыт" },
                   new Domain.Status { Id = "canceled", Name = "Отменен" },
                   new Domain.Status { Id = "in_progress", Name = "В работе" }
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
                await _freelanceDBContext.ReasonsToReport.AddRangeAsync(
                   new Domain.ReasonToReport { Name = "Общие вопросы" },
                   new Domain.ReasonToReport { Name = "Нарушение правил" },
                   new Domain.ReasonToReport { Name = "Другое" }
                );

                //var newUser = new ApplicationUser {
                //    UserName = "rezonint",
                //    Email = "rezonint@mail.ru",
                //    FirstName = "Anton",
                //    LastName = "Kunavin",
                //    AvatarProfilePath = "",
                //    HeaderProfilePath = "",
                //    RegisterDate = DateTime.Now,
                //    About = ""
                //};
                //var result = await _userManager.CreateAsync(newUser, "Qwerty123123");
                //await _userManager.AddToRoleAsync(newUser, "Manager");

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
