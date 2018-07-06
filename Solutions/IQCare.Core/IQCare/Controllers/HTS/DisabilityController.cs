using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/Disability")]
    public class DisabilityController : Controller
    {
        private readonly IMediator _mediator;

        public DisabilityController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetClientDisability/{personId}")]
        public async Task<IActionResult> GetClientDisability(int personId)
        {
            var response = await _mediator.Send(new GetClientDisabilityCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}