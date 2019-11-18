using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Otz;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Otz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OtzController : ControllerBase
    {
        private readonly IMediator _mediator;


        public OtzController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOtzActivityForm([FromBody]OtzActivityFormCommand otzActivityFormCommand)
        {
            var response = await _mediator.Send(otzActivityFormCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}