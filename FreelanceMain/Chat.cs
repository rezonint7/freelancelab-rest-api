using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class Chat {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AdminId { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
