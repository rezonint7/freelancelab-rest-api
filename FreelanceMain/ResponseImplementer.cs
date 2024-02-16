using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class ResponseImplementer {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ImplementerId { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Order Order { get; set; }
        public virtual Implementer Implementer { get; set; }
    }
}
