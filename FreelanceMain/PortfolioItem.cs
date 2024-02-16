using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class PortfolioItem {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ImplementerId { get; set; }
        public int CategoryId { get; set; }
        public virtual Implementer Implementer { get; set; }
        public virtual Category Category { get; set; }

    }
}
