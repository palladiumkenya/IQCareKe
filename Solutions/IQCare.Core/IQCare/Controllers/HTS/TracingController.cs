using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/hts/Tracing")]
    public class TracingController : Controller
    {
        private readonly IMediator _mediator;

        public TracingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetPersonTracingList/{personId}")]
        public async Task<IActionResult> GetPersonTracingList(int personId)
        {
            var response = await _mediator.Send(new GetPersonTracingListCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}