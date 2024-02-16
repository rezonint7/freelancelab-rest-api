using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class Customer {
        public Guid UserId { get; set; }
        public bool IsTrusted { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
