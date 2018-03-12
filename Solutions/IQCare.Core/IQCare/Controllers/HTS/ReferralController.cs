using System;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.Registration.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.HTS
{
    public class ReferralController : Controller
    {
        private readonly IMediator _mediator;

        public ReferralController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> ReferPatient(ReferPatientCommand command)
        {
            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> LinkPatient(AddLinkageCommand command)
        {
            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted);
            if (response.IsValid) return Ok();
            return BadRequest();
        }
    }
}