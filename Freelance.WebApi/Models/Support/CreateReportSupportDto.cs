using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Support.Commands.CreateReport;
using Freelance.Application.UserProfiles.ApplicationUsers.Commands.UpdateUserProfile;
using Freelance.WebApi.Models.Profiles;

namespace Freelance.WebApi.Models.Support {
    public class CreateReportSupportDto: IMapWith<CreateReportCommand> {
        public string ReportMessage { get; set; }
        public int ReasonId { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateReportSupportDto, CreateReportCommand>()
                .ForMember(cmd => cmd.ReportMessage, opt => opt.MapFrom(dto => dto.ReportMessage))
                .ForMember(cmd => cmd.ReasonId, opt => opt.MapFrom(dto => dto.ReasonId));
        }
    }
}
