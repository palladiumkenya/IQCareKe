using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;

namespace IQCare.Controllers.Afyamobile
{
    [Produces("application/json")]
    [Route("api/Hts")]
    public class HtsController : Controller
    {
        private readonly IMediator _mediator;

        public HtsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("indexclient")]
        public async Task<IActionResult> Post([FromBody] SynchronizeClientsCommand synchronizeClientsCommand)
        {
            var response = await _mediator.Send(synchronizeClientsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("partner")]
        public async Task<IActionResult> PostPartner([FromBody] SynchronizePartnersCommand synchronizePartnersCommand)
        {
            var response = await _mediator.Send(synchronizePartnersCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}