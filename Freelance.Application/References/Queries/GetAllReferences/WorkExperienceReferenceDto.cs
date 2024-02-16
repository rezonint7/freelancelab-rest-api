using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.References.Queries.GetAllReferences {
    public class WorkExperienceReferenceDto: IMapWith<WorkExperience> {
        public string Name { get; set; }
        public void Mapping(Profile profile) {
            profile.CreateMap<WorkExperience, WorkExperienceReferenceDto>()
                 .ForMember(workExp => workExp.Name,
                    opt => opt.MapFrom(workExp => workExp.Name));
        }
    }
}
