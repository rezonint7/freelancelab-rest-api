using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    public class CommentLookupDto : IMapWith<CommentToQuestionForum> {
        public string CommentMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserLookupDto User { get; set; }
        public IList<AnswerCommentLookupDto> Answers { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CommentToQuestionForum, CommentLookupDto>()
                .ForMember(comment => comment.CommentMessage,
                    opt => opt.MapFrom(comment => comment.CommentMessage))
                .ForMember(comment => comment.CreatedAt,
                    opt => opt.MapFrom(comment => comment.CreatedAt))
                .ForMember(comment => comment.UpdatedAt,
                    opt => opt.MapFrom(comment => comment.UpdatedAt))
                .ForMember(comment => comment.User,
                    opt => opt.MapFrom(comment => comment.User))
                .ForMember(comment => comment.Answers,
                    opt => opt.MapFrom(comment => comment.Answers));
        }
    }
}
