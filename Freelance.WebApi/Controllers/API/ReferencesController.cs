using AutoMapper;
using Freelance.Application.References.Queries.GetAllReferences;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API {
    [Route("api/references")]
    public class ReferencesController: BaseController {
        private readonly IMapper _mapper;
        public ReferencesController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ReferencesViewModel>> GetAllReferences() {
            var viewModel = await Mediator.Send(new GetAllReferencesQuery { });
            return Ok(viewModel);
        }
    }
}
