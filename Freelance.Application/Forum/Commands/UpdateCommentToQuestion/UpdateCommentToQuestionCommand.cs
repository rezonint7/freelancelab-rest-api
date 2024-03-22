using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.UpdateCommentToQuestion {
    public class UpdateCommentToQuestionCommand: IRequest<Unit> {
        public Guid UserId { get; set; }
        public int CommentId { get; set; }

        public string CommentMessage { get; set; }
    }
}
