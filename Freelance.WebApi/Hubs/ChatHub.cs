using AutoMapper;
using Freelance.Domain;
using Freelance.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.WebApi.Hubs {
    [EnableCors("AllowAll")]
    [Authorize(Roles = "Implementer, Customer, MANAGER, ADMIN, OWNER")]
    public class ChatHub : Hub {
        private readonly FreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;
        public ChatHub(FreelanceDBContext freelanceDBContext, IMapper mapper)
        {
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
        }

        public async Task SendMessage(string chatId, string messageContent) {
            var user = await _freelanceDBContext.Users.FirstOrDefaultAsync(user => user.Id == Guid.Parse(Context.UserIdentifier));
            var chat = await _freelanceDBContext.Chats.FindAsync(Guid.Parse(chatId));

            if (user != null && chat != null) {
                var message = new ChatMessage {
                    ChatId = chat.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    Content = messageContent,
                    IsRead = false
                };

                _freelanceDBContext.ChatMessages.Add(message);
                await _freelanceDBContext.SaveChangesAsync();

                await Clients.Group(chat.Id.ToString()).SendAsync("ReceiveMessage", new {
                    chatId = message.ChatId,
                    content = message.Content,
                    sender = user.UserName, // Assuming you have a UserName property
                    time = message.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
        }

        public async Task DeleteMessage(string messageId) {
            var message = await _freelanceDBContext.ChatMessages.FindAsync(Guid.Parse(messageId));

            if (message != null) {
                _freelanceDBContext.ChatMessages.Remove(message);
                await _freelanceDBContext.SaveChangesAsync();

                await Clients.Group(message.ChatId.ToString()).SendAsync("MessageDeleted", messageId);
            }
        }
        public async Task JoinChat(string chatId) {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
        public override async Task OnConnectedAsync() {
            await base.OnConnectedAsync();
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId)) {
                var user = await _freelanceDBContext.Users.FindAsync(Guid.Parse(userId));
                if (user != null) {
                    var chats = user.Chats.Select(c => c.Id.ToString());
                    foreach (var chatId in chats) {
                        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
                    }
                }
            }
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnectionId", Context.ConnectionId);
        }
    }
}
