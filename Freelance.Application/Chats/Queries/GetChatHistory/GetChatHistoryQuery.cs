﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatHistory {
    public class GetChatHistoryQuery: IRequest<ChatHistoryViewModel> {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
