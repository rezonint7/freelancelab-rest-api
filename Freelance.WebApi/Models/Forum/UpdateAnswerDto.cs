using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Forum.Commands.CreateNewAnswerToComment;
using Freelance.Application.Forum.Commands.UpdateAnswerToComment;

namespace Freelance.WebApi.Models.Forum {
    public class UpdateAnswerDto: IMapWith<UpdateAnswerToCommentCommand> {
        public int AnswerToCommentId { get; set; }
        public string AnswerMessage { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateAnswerDto, UpdateAnswerToCommentCommand>()
                .ForMember(answerCommand => answerCommand.AnswerToCommentId,
                    opt => opt.MapFrom(answerDto => answerDto.AnswerToCommentId))
                .ForMember(answerCommand => answerCommand.AnswerMessage,
                    opt => opt.MapFrom(answerDto => answerDto.AnswerMessage));
        }
    }
}
