using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Queries.GetOrderDetails {
    public class ResponseLookupDto: IMapWith<ResponseImplementer> {
        public Guid ImplementerId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? WorkExperience { get; set; }
        public string? Skills { get; set; }

        public string? Specialization { get; set; }
        public string? ResponseMessage { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ResponseImplementer, ResponseLookupDto>()
                .ForMember(impl => impl.ImplementerId,
                    opt => opt.MapFrom(impl => impl.Implementer.UserId))
                .ForMember(impl => impl.UserName,
                    opt => opt.MapFrom(impl => impl.Implementer.User.UserName))
                .ForMember(impl => impl.LastName,
                    opt => opt.MapFrom(impl => impl.Implementer.User.LastName))
                .ForMember(impl => impl.FirstName,
                    opt => opt.MapFrom(impl => impl.Implementer.User.FirstName))
                .ForMember(impl => impl.MiddleName,
                    opt => opt.MapFrom(impl => impl.Implementer.User.MiddleName))
                .ForMember(impl => impl.Specialization,
                    opt => opt.MapFrom(impl => impl.Implementer.Specialization))
                .ForMember(impl => impl.ResponseMessage,
                    opt => opt.MapFrom(impl => impl.ResponseMessage))
                .ForMember(impl => impl.WorkExperience,
                    opt => opt.MapFrom(impl => impl.Implementer.WorkExperience.Name))
                .ForMember(impl => impl.Skills,
                    opt => opt.MapFrom(impl => impl.Implementer.Skills))
                .ForMember(impl => impl.CreatedAt,
                    opt => opt.MapFrom(impl => impl.CreatedAt));
        }
    }
}
