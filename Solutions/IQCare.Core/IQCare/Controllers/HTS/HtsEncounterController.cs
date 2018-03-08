using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess;
using IQCare.HTS.BusinessProcess.Interfaces;
using IQCare.HTS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/HtsEncounter")]
    public class HtsEncounterController : Controller
    {
        private readonly IMediator _mediator;

        public HtsEncounterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEncounterCommand addEncounterCommand)
        {
            var response = await _mediator.Send(addEncounterCommand, Request.HttpContext.RequestAborted);
            if(response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}
