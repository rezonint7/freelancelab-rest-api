using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.Services {
    public class ChatService : IChatService {
        private readonly FreelanceDBContext _freelanceDBContext;
        private readonly UserService _userService;

        public ChatService(FreelanceDBContext freelanceDBContext, UserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Guid> CreateNewChatAsync(Guid implementerId, Guid customerId, Guid orderId, CancellationToken cancellationToken) {
            var implementer = await _userService.GetUserByIdAsync(implementerId, cancellationToken);
            var customer = await _userService.GetUserByIdAsync(customerId, cancellationToken);
            var order = await _freelanceDBContext.Orders.FirstOrDefaultAsync(order => order.OrderId == orderId);

            if (order == null) { throw new NotFoundException(nameof(Order), orderId); }
            if (implementer == null) { throw new NotFoundException(nameof(Implementer), implementerId); }
            if (customer == null) { throw new NotFoundException(nameof(Customer), customerId); }

            var chat = new Chat {
                Name = order.Title,
                AdminId = customerId,
                Users = new List<ApplicationUser> { customer, implementer }
            };
            await _freelanceDBContext.Chats.AddAsync(chat);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
