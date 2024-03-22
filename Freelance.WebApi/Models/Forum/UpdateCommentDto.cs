using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Forum.Commands.CreateNewCommentToQuestion;
using Freelance.Application.Forum.Commands.UpdateCommentToQuestion;

namespace Freelance.WebApi.Models.Forum {
    public class UpdateCommentDto: IMapWith<UpdateCommentToQuestionCommand> {
        public int CommentId { get; set; }
        public string CommentMessage { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<UpdateCommentDto, UpdateCommentToQuestionCommand>()
                .ForMember(commentCommand => commentCommand.CommentId,
                    opt => opt.MapFrom(commentDto => commentDto.CommentId))
                .ForMember(commentCommand => commentCommand.CommentMessage,
                    opt => opt.MapFrom(commentDto => commentDto.CommentMessage));
        }
    }
}
