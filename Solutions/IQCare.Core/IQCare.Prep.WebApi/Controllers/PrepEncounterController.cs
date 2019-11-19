using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Prep.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Prep.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrepEncounterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrepEncounterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> GetPrepEncounters([FromBody] GetPrepEncountersCommand getPrepEncountersCommand)
        {
            var response = await _mediator.Send(getPrepEncountersCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }



        [HttpPost]
        public async Task<IActionResult> GetPrepEncountersByVisitDate([FromBody] GetPrepEncountersByVisitDateCommand getPrepEncountersbyVisitDateCommand)
        {
            var response = await _mediator.Send(getPrepEncountersbyVisitDateCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetCorrectDisplayForm([FromBody] GetEncounterToFillCommand getEncounterToFillCommand)
        {
            var response = await _mediator.Send(getEncounterToFillCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
     
    }
}