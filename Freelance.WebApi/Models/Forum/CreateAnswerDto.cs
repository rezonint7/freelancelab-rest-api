using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Forum.Commands.CreateNewAnswerToComment;
using Freelance.Application.Forum.Commands.CreateNewCommentToQuestion;

namespace Freelance.WebApi.Models.Forum {
    public class CreateAnswerDto: IMapWith<CreateNewAnswerToCommentCommand> {
        public int CommentToQuestionForumId { get; set; }
        public string AnswerMessage { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateAnswerDto, CreateNewAnswerToCommentCommand>()
                .ForMember(answerCommand => answerCommand.CommentToQuestionForumId,
                    opt => opt.MapFrom(answerDto => answerDto.CommentToQuestionForumId))
                .ForMember(answerCommand => answerCommand.AnswerMessage,
                    opt => opt.MapFrom(answerDto => answerDto.AnswerMessage));
        }
    }
}
