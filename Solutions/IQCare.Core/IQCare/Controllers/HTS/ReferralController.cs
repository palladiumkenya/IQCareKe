using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/Referral")]
    public class ReferralController : Controller
    {
        private readonly IMediator _mediator;

        public ReferralController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> ReferPatient([FromBody]ReferPatientCommand command)
        {
            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok();
            return BadRequest();
        }

        [HttpPost("linkpatient")]
        public async Task<IActionResult> LinkPatient([FromBody]AddLinkageCommand command)
        {
            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}