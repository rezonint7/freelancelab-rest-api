using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class Feedback {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string FeedbackMessage { get; set; }
        public int FeedbackRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Order Order { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
