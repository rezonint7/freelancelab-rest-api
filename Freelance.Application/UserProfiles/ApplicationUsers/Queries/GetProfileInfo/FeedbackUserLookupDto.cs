using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo
{
    public class FeedbackUserLookupDto : IMapWith<Feedback>{
        public string FeedbackMessage { get; set; }
        public int FeedbackRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserName { get; set; }
        public string AvatarUrl { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Feedback, FeedbackUserLookupDto>()
                .ForMember(feedback => feedback.FeedbackMessage,
                    opt => opt.MapFrom(feedback => feedback.FeedbackMessage))
                .ForMember(feedback => feedback.FeedbackRating,
                    opt => opt.MapFrom(feedback => feedback.FeedbackRating))
                .ForMember(feedback => feedback.CreatedAt,
                    opt => opt.MapFrom(feedback => feedback.CreatedAt))
                .ForMember(feedback => feedback.UpdatedAt, 
                    opt => opt.MapFrom(feedback => feedback.UpdatedAt))
				.ForMember(feedback => feedback.UserName,
					opt => opt.MapFrom(feedback => feedback.User.UserName))
				.ForMember(feedback => feedback.AvatarUrl,
					opt => opt.MapFrom(feedback => feedback.User.AvatarProfilePath));
        }
    }
}
