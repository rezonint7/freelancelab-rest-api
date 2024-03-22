using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    public class AnswerCommentLookupDto: IMapWith<AnswerToComment> {
        public string AnswerMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserLookupDto User { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<AnswerToComment, AnswerCommentLookupDto>()
                .ForMember(answer => answer.AnswerMessage,
                    opt => opt.MapFrom(answer => answer.AnswerMessage))
                .ForMember(answer => answer.CreatedAt,
                    opt => opt.MapFrom(answer => answer.CreatedAt))
                .ForMember(answer => answer.UpdatedAt,
                    opt => opt.MapFrom(answer => answer.UpdatedAt))
                .ForMember(answer => answer.User,
                    opt => opt.MapFrom(answer => answer.User));
        }
    }
}
