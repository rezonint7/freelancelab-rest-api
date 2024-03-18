using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewCommentToQuestion {
    public class CreateNewCommentToQuestionCommand: IRequest<int> {
        public Guid UserId { get; set; }
        public int QuestionForumId { get; set; }

        public string CommentMessage { get; set; }
    }
}
