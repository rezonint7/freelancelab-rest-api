using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatHistory {
    public class ChatHistoryViewModel {
        public string ChatName { get; set; }
        public IList<ChatMessageLookupDto> ChatHistory { get; set; }
        public IList<ChatUserLookupDto> ChatUsers { get; set; }
    }
}
