using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Chats.Queries.GetChatList;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Chats.Queries.GetChatHistory {
    internal class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryQuery, ChatHistoryViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetChatHistoryQueryHandler(IFreelanceDBContext freelanceDBContext, IUserService userService, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ChatHistoryViewModel> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var chat = await _freelanceDBContext.Chats
                .Include(i => i.Users)
                .Include(i => i.ChatMessages)
                .FirstOrDefaultAsync(chat => chat.Id == request.ChatId);
            if (chat == null) { throw new NotFoundException(nameof(Chat), request.ChatId); }
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }
            if (!chat.Users.Contains(user)) { throw new NotFoundException(nameof(Chat), request.ChatId); }


            var messages = await ((chat.ChatMessages).AsQueryable())
                .ProjectTo<ChatMessageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var users = await ((chat.Users).AsQueryable())
                .ProjectTo<ChatUserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ChatHistoryViewModel {
                ChatName = chat.Name,
                ChatHistory = messages,
                ChatUsers = users
            };
        }
    }
}
