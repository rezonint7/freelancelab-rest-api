using AutoMapper;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatHistory {
    public class ChatMessageLookupDto: IMapWith<ChatMessage> {
        public Guid MessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        public ChatUserLookupDto User { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ChatMessage, ChatMessageLookupDto>()
               .ForMember(chatMessage => chatMessage.MessageId,
                   opt => opt.MapFrom(chatMessage => chatMessage.Id))
               .ForMember(chatMessage => chatMessage.CreatedAt,
                   opt => opt.MapFrom(chatMessage => chatMessage.CreatedAt))
               .ForMember(chatMessage => chatMessage.UpdatedAt,
                   opt => opt.MapFrom(chatMessage => chatMessage.UpdatedAt))
               .ForMember(chatMessage => chatMessage.Content,
                   opt => opt.MapFrom(chatMessage => chatMessage.Content))
               .ForMember(chatMessage => chatMessage.User,
                   opt => opt.MapFrom(chatMessage => chatMessage.User));
        }
    }
}
