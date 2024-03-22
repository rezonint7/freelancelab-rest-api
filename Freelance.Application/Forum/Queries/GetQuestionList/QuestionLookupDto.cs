using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionList {
    public class QuestionLookupDto: IMapWith<QuestionForum> {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<QuestionForum, QuestionLookupDto>()
                .ForMember(question => question.QuestionId,
                    opt => opt.MapFrom(question => question.Id))
                .ForMember(question => question.Title,
                    opt => opt.MapFrom(question => question.Title))
                .ForMember(question => question.Tags,
                    opt => opt.MapFrom(question => question.Tags))
                .ForMember(question => question.CreatedAt,
                    opt => opt.MapFrom(question => question.CreatedAt))
                .ForMember(question => question.LikesCount,
                    opt => opt.MapFrom(question => question.LikesBy.Split(';', StringSplitOptions.RemoveEmptyEntries).Length))
                .ForMember(question => question.CommentsCount,
                    opt => opt.MapFrom(question => question.Comments.Count));
        }
    }
}
