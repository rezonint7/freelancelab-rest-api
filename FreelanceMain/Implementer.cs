using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class Implementer {
        public Guid UserId { get; set; }
        public string? Specialization {  get; set; }
        public string? Skills { get; set; }

        public int WorkExperienceId { get; set; }
        public int CategoryId { get; set; }
        

        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual WorkExperience WorkExperience { get; set; }

        public ICollection<PortfolioItem> Portfolio { get; set; } = new HashSet<PortfolioItem>();
        public ICollection<ResponseImplementer> Responses { get; set; } = new HashSet<ResponseImplementer>();
    }
}
