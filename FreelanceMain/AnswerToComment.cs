using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class AnswerToComment {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CommentToQuestionForumId { get; set; }
        public string AnswerMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual CommentToQuestionForum CommentToQuestionForum { get; set; }
    }
}
