using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo
{
    public class CustomerInfoViewModel : IMapWith<Customer>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string AvatarProfilePath { get; set; }
        public string HeaderProfilePath { get; set; }
        public string About { get; set; }
        public double Rating { get; set; }

        public IList<OrderLookupDto> Orders { get; set; }
        public IList<FeedbackUserLookupDto> Feedbacks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerInfoViewModel>()
                .ForMember(customerViewModel => customerViewModel.UserId,
                    opt => opt.MapFrom(customer => customer.UserId))
                .ForMember(customerViewModel => customerViewModel.UserName,
                    opt => opt.MapFrom(customer => customer.User.UserName))
                .ForMember(customerViewModel => customerViewModel.FirstName,
                    opt => opt.MapFrom(customer => customer.User.FirstName))
                .ForMember(customerViewModel => customerViewModel.LastName,
                    opt => opt.MapFrom(customer => customer.User.LastName))
                .ForMember(customerViewModel => customerViewModel.MiddleName,
                    opt => opt.MapFrom(customer => customer.User.MiddleName))
                .ForMember(customerViewModel => customerViewModel.Birthday,
                    opt => opt.MapFrom(customer => customer.User.Birthday))
                .ForMember(customerViewModel => customerViewModel.RegisterDate,
                    opt => opt.MapFrom(customer => customer.User.RegisterDate))
                .ForMember(customerViewModel => customerViewModel.AvatarProfilePath,
                    opt => opt.MapFrom(customer => customer.User.AvatarProfilePath))
                .ForMember(customerViewModel => customerViewModel.HeaderProfilePath,
                    opt => opt.MapFrom(customer => customer.User.HeaderProfilePath))
                .ForMember(customerViewModel => customerViewModel.About,
                    opt => opt.MapFrom(customer => customer.User.About))
                .ForMember(customerViewModel => customerViewModel.Rating,
                    opt => opt.MapFrom(customer => customer.User.Rating))
                .ForMember(customerViewModel => customerViewModel.Orders,
                    opt => opt.MapFrom(customer => customer.Orders))
                .ForMember(customerViewModel => customerViewModel.Feedbacks,
                    opt => opt.MapFrom(customer => customer.User.Feedbacks));
        }

    }
}
