using Microsoft.AspNetCore.Mvc;
using Freelance.Application.Orders.Queries.GetOrderList;
using Freelance.Application.Orders.Queries.GetOrderDetails;
using System.Threading.Tasks;
using AutoMapper;
using Freelance.Application.Orders.Commands.CreateOrder;
using Freelance.Application.Orders.Commands.UpdateOrder;
using Freelance.Application.Orders.Commands.DeleteOrder;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Freelance.WebApi.Models.Orders;
using Freelance.Application.Orders.Commands.SetImplementerToOrder;
using Freelance.Application.Orders.Commands.DeleteImplementerFromOrder;
using Freelance.Application.Orders.Commands.CompleteOrder;
using Freelance.Application.Orders.Commands.DeleteResponseImplementer;
using Freelance.Application.Orders.Commands.CreateResponseImplementer;

namespace Freelance.WebApi.Controllers.API
{
    [Route("api/orders")]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        public OrderController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<OrderListViewModel>> GetAllOrders(string? search = "", string categories = "-1", string additionalCategories = "null", int pageSize = 20, int page = 1)
        {
            var query = new GetOrderListQuery {
                Search = search,
                Categories = categories,
                AdditionalCategories = additionalCategories,
                Page = page,
                PageSize = pageSize
            };

            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<OrderDetailsViewModel>> GetDetailsOrder(Guid id)
        {
            var query = new GetOrderDetailsQuery{ OrderId = id, UserId = UserId };
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Guid>> CreateNewOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var command = _mapper.Map<CreateNewOrderCommand>(createOrderDto);
            command.CustomerId = UserId;

            var orderId = await Mediator.Send(command);

            return Created($"{orderId}", orderId);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Customer, Admin, Owner")]
        public async Task<ActionResult<Unit>> UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        {
            var command = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
            command.CustomerId = UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Customer, Admin, Owner")]
        public async Task<ActionResult<Unit>> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand
            {
                OrderId = id,
                CustomerId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/response")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Guid>> CreateNewResponseOrder(Guid id, [FromBody] CreateResponseDto createResponseDto)
        {
            var command = new CreateNewResponseCommand
            {
                OrderId = id,
				ResponseMessage = createResponseDto.ResponseMessage,
                ImplementerId = UserId
            };
            await Mediator.Send(command);

            return Created($"Order: {id} - Implementer: {UserId}", true);
        }

        [HttpDelete("{id}/delete-response")]
        [Authorize(Roles = "Implementer")]
        public async Task<ActionResult<Unit>> DeleteResponseOrder(Guid id)
        {
            var command = new DeleteResponseCommand { ResponseId = id, ImplementerId = UserId };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("set-implementer")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Unit>> SetImplementerToOrder([FromBody] SetImplementerToOrderDto setImplementerDto) {
            var command = new SetImplementerToOrderCommand {
                OrderId = setImplementerDto.OrderId,
                ImplementerId = setImplementerDto.ImplementerId,
                CustomerId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete-implementer")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Unit>> DeleteImplementerFromOrder([FromBody] DeleteImplementerFromOrderDto deleteImplementerFromOrder) {
            var command = new DeleteImplementerFromOrderCommand {
                OrderId = deleteImplementerFromOrder.OrderId,
                ImplementerId = deleteImplementerFromOrder.ImplementerId,
                CustomerId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("complete-order/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Unit>> CompleteOrder(Guid id) {
            var command = new CompleteOrderCommand {
                OrderId = id,
                CustomerId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
