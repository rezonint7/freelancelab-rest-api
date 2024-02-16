using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class UserWarning {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ReasonWarningId { get; set; }
        public string MessageWarning { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ReasonToReport ReasonWarning { get; set; }

    }
}
