
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.References.Queries.GetAllReferences {
    public class ReferencesViewModel {
        public IList<CategoryReferenceDto> Categories { get; set; }
        public IList<WorkExperienceReferenceDto> WorkExperience { get; set; }
        public IList<CurrencyReferenceDto> Currencies { get; set; }
    }
}
