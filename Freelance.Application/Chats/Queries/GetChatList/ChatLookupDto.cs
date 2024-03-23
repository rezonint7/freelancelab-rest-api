using AutoMapper;
using Freelance.Application.Chats.Queries.GetChatHistory;
using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatList {
    public class ChatLookupDto: IMapWith<Chat> {
        public Guid ChatId { get; set; }
        public string Name { get; set; }

        public ChatUserLookupDto Admin { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Chat, ChatLookupDto>()
               .ForMember(chat => chat.ChatId,
                   opt => opt.MapFrom(chat => chat.Id))
               .ForMember(chat => chat.Name,
                   opt => opt.MapFrom(chat => chat.Name))
               .ForMember(chat => chat.Admin,
                   opt => opt.MapFrom(chat => chat.Users.FirstOrDefault(user => user.Id == chat.AdminId)));
        }
    }
}
