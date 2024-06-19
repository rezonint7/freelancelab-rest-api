using AutoMapper;
using Freelance.Application.Orders.Commands.DeleteOrder;
using Freelance.Application.Support.Commands.CloseReport;
using Freelance.Application.Support.Commands.ConfirmReport;
using Freelance.Application.Support.Commands.CreateReport;
using Freelance.Application.Support.Queries.GetListReports;
using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetProfileInfo;
using Freelance.Domain;
using Freelance.WebApi.Models.Support;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Freelance.WebApi.Controllers.API {
    [Route("api/support")]
    public class SupportController: BaseController {
        private readonly IMapper _mapper;
        public SupportController(IMapper mapper) => _mapper = mapper;

        [HttpGet("reports")]
        [Authorize(Roles = "Manager, Admin, Owner")]
        public async Task<ActionResult<ReportsListViewModel>> GetReportsList(int page = 1) {
            var query = new GetListReportsQuery { Page = page, PageSize = 20 };
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPut("reports/{id}/confirm")]
        [Authorize(Roles = "Manager, Admin, Owner")]
        public async Task<ActionResult<Unit>> ConfirmReport(int id) {
            var command = new ConfirmReportCommand {
                SupportId = UserId,
                ReportId= id
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("reports/{id}/close")]
        [Authorize(Roles = "Manager, Admin, Owner")]
        public async Task<ActionResult<Unit>> CloseReport(int id) {
            var command = new CloseReportCommand {
                ReportId = id
            };
            await Mediator.Send(command);

            return NoContent();
        }


        [HttpPost("reports/create")]
        [Authorize(Roles = "Implementer, Customer")]
        public async Task<ActionResult<Unit>> CreateReport([FromBody] CreateReportSupportDto createReportSupportDto) {
            var command = new CreateReportCommand {
                UserId = UserId,
                ReportMessage = createReportSupportDto.ReportMessage,
                ReasonId = createReportSupportDto.ReasonId,
            };
            var reportId = await Mediator.Send(command);

            return Created($"{reportId}", reportId);
        }
    }
}
