using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatList {
    public class GetChatListQuery: IRequest<ChatListViewModel> {
        public Guid UserId { get; set; }
    }
}
