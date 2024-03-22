using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Forum.Commands.CreateNewQuestionForum;
using Freelance.Application.Orders.Commands.CreateOrder;
using Freelance.WebApi.Models.Orders;

namespace Freelance.WebApi.Models.Forum {
    public class CreateQuestionDto: IMapWith<CreateNewQuestionForumCommand> {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateQuestionDto, CreateNewQuestionForumCommand>()
                .ForMember(questionCommand => questionCommand.Title,
                    opt => opt.MapFrom(questionDto => questionDto.Title))
                .ForMember(questionCommand => questionCommand.Content,
                    opt => opt.MapFrom(questionDto => questionDto.Content))
                .ForMember(questionCommand => questionCommand.Tags,
                    opt => opt.MapFrom(questionDto => questionDto.Tags));
        }
    }
}
