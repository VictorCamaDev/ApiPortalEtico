using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using ApiPortalEtico.Application.IrregularityReports.Commands;
using ApiPortalEtico.Application.IrregularityReports.Queries;

namespace ApiPortalEtico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IrregularityReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IrregularityReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllIrregularityReportsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetIrregularityReportByIdQuery { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIrregularityReportCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}

