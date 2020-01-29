using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/ServiceArea")]
    public class ServiceAreaController : Controller
    {
        private readonly IMediator _mediator;

        public ServiceAreaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetServiceAreasCommand(), Request.HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetServiceArea")]
        public async Task<IActionResult> GetServiceArea(string name)
        {
            var results = await _mediator.Send(new GetServiceAreaByName{Name = name}, Request.HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetAllIdentifiers")]
        public async Task<IActionResult> GetAllIdentifiers()
        {
            var result = await _mediator.Send(new GetIdentifiersCommand(), Request.HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);
            return BadRequest(result);
        }
    }
}