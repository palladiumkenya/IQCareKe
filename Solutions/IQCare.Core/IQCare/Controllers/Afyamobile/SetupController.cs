using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.BusinessProcess.Commands.Setup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Afyamobile
{
    [Produces("application/json")]
    [Route("api/Setup")]
    public class SetupController : Controller
    {
        private readonly IMediator _mediator;

        public SetupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("getFacilities")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetActiveFacilitiesCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetActiveUsersCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("htsOptions")]
        public async Task<IActionResult> HtsOptions()
        {
            var options = new string[]
            {
                "Gender", "MaritalStatus", "TracingOutcome", "TracingMode", "HIVTestKits", "HIVResults", "YesNo",
                "Strategy", "Disabilities", "TestedAs", "TbScreening", "HTSEntryPoints", "HIVFinalResults", "YesNoNA",
                "HTSKeyPopulation", "Relationship", "HivStatus", "HivCareStatus", "ScreeningHivStatus",
                "PnsTracingOutcome", "IpvOutcome", "PNSRealtionship", "PNSRelationship", "YesNoDeclined", "PnsApproach"
            };
            var response = await _mediator.Send(new GetAfyaMobileLookupsCommand()
            {
                options = options
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}