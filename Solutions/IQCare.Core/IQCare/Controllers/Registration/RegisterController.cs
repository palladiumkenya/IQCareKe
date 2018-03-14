using IQCare.Common.BusinessProcess.Commands;
using IQCare.Registration.BusinessProcess.Commands.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQCare.Controllers.Registration
{
    [Produces("application/json")]
    [Route("api/Register")]
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterClientCommand registerClientCommand)
        {
            var response = await _mediator.Send(registerClientCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("enrollment")]
        public async Task<IActionResult> Post([FromBody] EnrollClientCommand enrollClientCommand)
        {
            var response = await _mediator.Send(enrollClientCommand, Request.HttpContext.RequestAborted);
            if(response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}