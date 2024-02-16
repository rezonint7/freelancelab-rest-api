using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Application.customers.Queries.GetcustomerList;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.References.Queries.GetAllReferences {
    public class CategoryReferenceDto: IMapWith<Category> {
        public string Name { get; set; }
        public void Mapping(Profile profile) {
            profile.CreateMap<Category, CategoryReferenceDto>()
                 .ForMember(category => category.Name,
                    opt => opt.MapFrom(category => category.Name));
        }
    }
}
