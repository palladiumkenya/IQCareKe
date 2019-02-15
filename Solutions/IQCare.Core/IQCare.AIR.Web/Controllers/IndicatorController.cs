using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.AIR.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class IndicatorController : Controller
    {
        private readonly IMediator _mediator;
        public IndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddResults([FromBody] SubmitIndicatorResultsCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }
    }
}
