using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.Application.Chats.Queries.GetChatList {
    internal class GetChatListQueryHandler : IRequestHandler<GetChatListQuery, ChatListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetChatListQueryHandler(IFreelanceDBContext freelanceDBContext, IUserService userService, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ChatListViewModel> Handle(GetChatListQuery request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            var chats = await (_freelanceDBContext.Chats.Where(i => i.Users.Contains(user)))
                .ProjectTo<ChatLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ChatListViewModel { Chats = chats };
        }
    }
}
