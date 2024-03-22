using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewAnswerToComment {
    public class CreateNewAnswerToCommentCommand: IRequest<int> {
        public Guid UserId { get; set; }
        public int CommentToQuestionForumId { get; set; }
        public string AnswerMessage { get; set; }
    }
}
