using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Queries.GetListReports {
    public class ReportLookupDto: IMapWith<ReportToSupport> {
        public int Id { get; set; }
        public string ReportMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

        public UserLookupDto User { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ReportToSupport, ReportLookupDto>()
                .ForMember(report => report.Id,
                    opt => opt.MapFrom(report => report.Id))
                .ForMember(report => report.ReportMessage,
                    opt => opt.MapFrom(report => report.ReportMessage))
                .ForMember(report => report.CreatedAt,
                    opt => opt.MapFrom(report => report.CreatedAt))
                .ForMember(report => report.Reason,
                    opt => opt.MapFrom(report => report.Reason.Name))
                .ForMember(report => report.Status,
                    opt => opt.MapFrom(report => report.Status.Id))
                .ForMember(report => report.User,
                    opt => opt.MapFrom(report => report.User));
        }

    }
}
