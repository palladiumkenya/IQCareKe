using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/Facility")]
    public class FacilityController : Controller
    {
        private readonly IMediator _mediatR;

        public FacilityController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _mediatR.Send(new GetFacilityListCommand { }, HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("GetActiveFacility")]
        public async Task<object> GetActiveFacility()
        {
            var response = await _mediatR.Send(new GetActiveFaciltyCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

       

       
    }
}
