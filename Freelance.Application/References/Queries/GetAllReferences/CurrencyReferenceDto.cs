using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.References.Queries.GetAllReferences {
    public class CurrencyReferenceDto: IMapWith<Currency> {
        public string Name { get; set; }
        public string Code { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Currency, CurrencyReferenceDto>()
                 .ForMember(currency => currency.Name,
                    opt => opt.MapFrom(currency => currency.Name))
                 .ForMember(currency => currency.Code,
                    opt => opt.MapFrom(currency => currency.Code));
        }
    }
}
