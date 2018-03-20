using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.BusinessProcess.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/Lookup")]
    public class LookupController : Controller
    {
        private readonly IMediator _mediator;

        public LookupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("byGroupName")]
        public async Task<IActionResult> Get(string groupName)
        {
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand {GroupName = groupName},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("htsOptions")]
        public async Task<IActionResult> Get()
        {
            string[] options = new string[] { "HTSEntryPoints", "YesNo", "Disabilities", "TestedAs", "Strategy", "TBStatus", "ReasonsPartner", "HIVResults", "HIVTestKits", "HIVFinalResults" };

            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);


            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("optionsByGroupandItemName")]
        public async Task<IActionResult> Get(string groupName, string itemName)
        {
            var results =
                await _mediator.Send(
                    new GetOptionsByGroupAndItemNameCommand {GroupName = groupName, ItemName = itemName},
                    HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }


        [HttpGet("registrationOptions")]
        public async Task<IActionResult> GetOptions()
        {
            string[] options = new string[] { "MaritalStatus", "KeyPopulation", "Gender" };
            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("htsTracingOptions")]
        public async Task<IActionResult> GetTracingOptions()
        {
            string[] options = new string[] { "TracingMode", "TracingOutcome" };
            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }
    }
}