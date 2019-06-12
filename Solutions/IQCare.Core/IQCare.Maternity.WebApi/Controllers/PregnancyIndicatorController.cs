using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PregnancyIndicatorController : ControllerBase
    {
        IMediator _mediator;

        public PregnancyIndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPregnancyIndicator([FromBody]AddPregnancyIndicatorCommand addPregnancyIndicatorCommand)
        {
            var result = await _mediator.Send(addPregnancyIndicatorCommand, Request.HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);
            return BadRequest(result);
        }
    }
}