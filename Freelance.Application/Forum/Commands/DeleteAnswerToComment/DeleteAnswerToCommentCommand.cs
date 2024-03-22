using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.DeleteAnswerToComment {
    public class DeleteAnswerToCommentCommand: IRequest<Unit> {
        public Guid UserId { get; set; }
        public int AnswerId { get; set; }
    }
}
