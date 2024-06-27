using AutoMapper;
using Freelance.Application.Chats.Queries.GetChatHistory;
using Freelance.Application.Chats.Queries.GetChatList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API {
    [Route("api/chats")]
    public class ChatController : BaseController {
        private readonly IMapper _mapper;
        public ChatController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [Authorize(Roles = "Implementer, Customer, Manager, Admin, Owner")]
        public async Task<ActionResult<ChatListViewModel>> GetAllChats() {
            var query = new GetChatListQuery { UserId = UserId };
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpGet("details/{id}")]
        [Authorize(Roles = "Implementer, Customer, Manager, Admin, Owner")]
        public async Task<ActionResult<ChatHistoryViewModel>> GetChatHistory(Guid id) {
            var query = new GetChatHistoryQuery { UserId = UserId, ChatId = id };
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }
    }
}
