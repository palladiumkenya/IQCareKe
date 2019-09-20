using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using IQCare.Pharm.BusinessProcess.CommandHandlers;
using IQCare.Pharm.BusinessProcess.Commands.Lookup;
using IQCare.Pharm.BusinessProcess.Queries;

namespace IQCare.Pharm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class LookupPharmacyController : ControllerBase
    {

        private readonly IMediator _mediator;
        // GET: api/Lookup

        public LookupPharmacyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet("getFrequency")]
        public async Task<IActionResult> GetFrequency()
        {
            var results = await _mediator.Send(new GetLookupFrequencyCommand(),
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }




        [HttpGet("getActiveFacilityModules/{Id}")]
        public async Task<IActionResult> GetActiveFacilityModules(int Id)
        {
            var response = await _mediator.Send(new GetFacilityModuleQuery{ LocationId= Id}, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }
    }

    
}
