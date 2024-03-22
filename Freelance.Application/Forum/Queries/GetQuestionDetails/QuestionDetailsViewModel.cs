using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    public class QuestionDetailsViewModel: IMapWith<QuestionForum> {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string LikesBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserLookupDto User { get; set; }

        public IList<CommentLookupDto> Comments { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<QuestionForum, QuestionDetailsViewModel>()
                .ForMember(questionViewModel => questionViewModel.Title,
                    opt => opt.MapFrom(question => question.Title))
                .ForMember(questionViewModel => questionViewModel.Content,
                    opt => opt.MapFrom(question => question.Content))
                .ForMember(questionViewModel => questionViewModel.CreatedAt,
                    opt => opt.MapFrom(question => question.CreatedAt))
                .ForMember(questionViewModel => questionViewModel.UpdatedAt,
                    opt => opt.MapFrom(question => question.UpdatedAt))
                .ForMember(questionViewModel => questionViewModel.Tags,
                    opt => opt.MapFrom(question => question.Tags))
                .ForMember(questionViewModel => questionViewModel.LikesBy,
                    opt => opt.MapFrom(question => question.LikesBy))
                .ForMember(questionViewModel => questionViewModel.User,
                    opt => opt.MapFrom(question => question.User))
                .ForMember(questionViewModel => questionViewModel.Comments,
                    opt => opt.MapFrom(question => question.Comments));
        }
    }
}
