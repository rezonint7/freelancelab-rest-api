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
    public class ImplementerInfoViewModel : IMapWith<Implementer> {
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

        public string? Specialization { get; set; }
        public string? Skills { get; set; }
        public double Rating { get; set; }

        public Category Category { get; set; }
        public WorkExperience WorkExperience { get; set; }

        public IList<PortfolioImplementerLookupDto> Portfolio { get; set; }
        public IList<OrderResponseLookupDto> Responses { get; set; }
        public IList<FeedbackUserLookupDto> Feedbacks { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Implementer, ImplementerInfoViewModel>()
                .ForMember(implementerViewModel => implementerViewModel.UserId,
                    opt => opt.MapFrom(implementer => implementer.UserId))
                .ForMember(implementerViewModel => implementerViewModel.UserName,
                    opt => opt.MapFrom(implementer => implementer.User.UserName))
                .ForMember(implementerViewModel => implementerViewModel.FirstName,
                    opt => opt.MapFrom(implementer => implementer.User.FirstName))
                .ForMember(implementerViewModel => implementerViewModel.LastName,
                    opt => opt.MapFrom(implementer => implementer.User.LastName))
                .ForMember(implementerViewModel => implementerViewModel.MiddleName,
                    opt => opt.MapFrom(implementer => implementer.User.MiddleName))
                .ForMember(implementerViewModel => implementerViewModel.Birthday,
                    opt => opt.MapFrom(implementer => implementer.User.Birthday))
                .ForMember(implementerViewModel => implementerViewModel.RegisterDate,
                    opt => opt.MapFrom(implementer => implementer.User.RegisterDate))
                .ForMember(implementerViewModel => implementerViewModel.AvatarProfilePath,
                    opt => opt.MapFrom(implementer => implementer.User.AvatarProfilePath))
                .ForMember(implementerViewModel => implementerViewModel.HeaderProfilePath,
                    opt => opt.MapFrom(implementer => implementer.User.HeaderProfilePath))
                .ForMember(implementerViewModel => implementerViewModel.Rating,
                    opt => opt.MapFrom(implementer => implementer.User.Rating))
                .ForMember(implementerViewModel => implementerViewModel.About,
                    opt => opt.MapFrom(implementer => implementer.User.About))
                .ForMember(implementerViewModel => implementerViewModel.Specialization,
                    opt => opt.MapFrom(implementer => implementer.Specialization))
                .ForMember(implementerViewModel => implementerViewModel.Skills,
                    opt => opt.MapFrom(implementer => implementer.Skills))
                .ForMember(implementerViewModel => implementerViewModel.Category,
                    opt => opt.MapFrom(implementer => implementer.Category))
                .ForMember(implementerViewModel => implementerViewModel.WorkExperience,
                    opt => opt.MapFrom(implementer => implementer.WorkExperience))
                .ForMember(implementerViewModel => implementerViewModel.Portfolio,
                    opt => opt.MapFrom(implementer => implementer.Portfolio))
                .ForMember(implementerViewModel => implementerViewModel.Responses,
                    opt => opt.MapFrom(implementer => implementer.Responses))
                .ForMember(implementerViewModel => implementerViewModel.Feedbacks,
                    opt => opt.MapFrom(implementer => implementer.User.Feedbacks));
        }
    }
}
