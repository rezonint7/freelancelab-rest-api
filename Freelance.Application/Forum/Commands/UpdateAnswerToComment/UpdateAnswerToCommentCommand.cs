using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.UpdateAnswerToComment {
    public class UpdateAnswerToCommentCommand: IRequest<Unit> {
        public Guid UserId { get; set; }
        public int AnswerToCommentId { get; set; }
        public string AnswerMessage { get; set; }
    }
}
