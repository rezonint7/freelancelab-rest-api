﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.DeleteCommentToQuestion {
    internal class DeleteCommentToQuestionCommand: IRequest<Unit> {
        public Guid UserId { get; set; }
        public int CommentId { get; set; }
    }
}
