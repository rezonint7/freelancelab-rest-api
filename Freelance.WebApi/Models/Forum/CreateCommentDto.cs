using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Forum.Commands.CreateNewCommentToQuestion;
using Freelance.Application.Forum.Commands.CreateNewQuestionForum;

namespace Freelance.WebApi.Models.Forum {
    public class CreateCommentDto: IMapWith<CreateNewCommentToQuestionCommand> {
        public int QuestionForumId { get; set; }
        public string CommentMessage { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateCommentDto, CreateNewCommentToQuestionCommand>()
                .ForMember(commentCommand => commentCommand.QuestionForumId,
                    opt => opt.MapFrom(commentDto => commentDto.QuestionForumId))
                .ForMember(commentCommand => commentCommand.CommentMessage,
                    opt => opt.MapFrom(commentDto => commentDto.CommentMessage));
        }
    }
}
