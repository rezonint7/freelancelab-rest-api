using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.customers.Queries.GetcustomerList {
    public class CustomerLookupDto: IMapWith<Customer> {
        public Guid CustomerId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }

        public bool IsTrusted { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Customer, CustomerLookupDto>()
                .ForMember(customer => customer.CustomerId,
                    opt => opt.MapFrom(customer => customer.UserId))
                .ForMember(customer => customer.UserName,
                    opt => opt.MapFrom(customer => customer.User.UserName))
                .ForMember(customer => customer.FirstName,
                    opt => opt.MapFrom(customer => customer.User.FirstName))
                .ForMember(customer => customer.LastName,
                    opt => opt.MapFrom(customer => customer.User.LastName))
                .ForMember(customer => customer.MiddleName,
                    opt => opt.MapFrom(customer => customer.User.MiddleName))
                .ForMember(customer => customer.IsTrusted,
                    opt => opt.MapFrom(customer => customer.IsTrusted));
        }
    }
}
