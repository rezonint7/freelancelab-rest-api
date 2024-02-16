using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class ReportToSupport {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ReportMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReasonId { get; set; }
        public bool Status { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ReasonToReport Reason { get; set; }
    }
}
