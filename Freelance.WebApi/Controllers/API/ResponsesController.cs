using AutoMapper;
using Freelance.Application.ResponsesImplOrders.Commands.CreateResponseImplementer;
using Freelance.Application.ResponsesImplOrders.Commands.DeleteResponseImplementer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API {
    [Route("api/response")]
    public class ResponsesContoller: BaseController {

        private readonly IMapper _mapper;
        public ResponsesContoller(IMapper mapper) => _mapper = mapper;

        [HttpPost("{orderId}")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> ResponseToOrder(Guid orderId) {
            var command = new CreateNewResponseCommand { OrderId = orderId };
            command.ImplementerId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete/{orderId}")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> DeleteResponse(Guid responseId) {
            var command = new DeleteResponseCommand { ResponseId = responseId };
            command.ImplementerId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
