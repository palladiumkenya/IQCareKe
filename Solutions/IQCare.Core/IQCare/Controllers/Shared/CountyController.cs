using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/County")]
    public class CountyController : Controller
    {
        private readonly IMediator _mediator;

        public CountyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("getCounties")]
        public async Task<IActionResult> GetCounties()
        {
            var counties = await _mediator.Send(new GetAllCountiesCommand() { }, Request.HttpContext.RequestAborted);
            if (counties.IsValid)
                return Ok(counties.Value);
            return BadRequest(counties);
        }


        [HttpGet("getSubCounties/{countyId}")]
        public async Task<IActionResult> GetSubCounties(int countyId)
        {
            var subcounties = await _mediator.Send(new GetSubCountiesCommand() { CountyId = countyId }, HttpContext.RequestAborted);
            if (subcounties.IsValid)
                return Ok(subcounties.Value);
            return BadRequest(subcounties);
        }

        [HttpGet("getWards/{subcountyid}")]
        public async Task<IActionResult> GetWardList(int subcountyid)
        {
            var results = await _mediator.Send(new GetWardCommand() { SubcountyId = subcountyid }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }
    }
}