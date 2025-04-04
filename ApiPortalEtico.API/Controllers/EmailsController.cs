using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Emails.Commands;
using MediatR;

namespace ApiPortalEtico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("EnviarEmail")]
        public async Task<IActionResult> SendIrregularityReportEmail(SendIrregularityReportEmailCommand command)
        {
            // Validate request
            if (command.IrregularityReportId <= 0)
            {
                return BadRequest("Id Invalido");
            }

            if (command.Recipients == null || command.Recipients.Count == 0)
            {
                return BadRequest("Se requiere un receptor como mínimo");
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}

