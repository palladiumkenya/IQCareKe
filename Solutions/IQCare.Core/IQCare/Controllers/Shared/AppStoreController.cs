using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/AppStore")]
    public class AppStoreController : Controller
    {
        private readonly IMediator _mediator;

        public AppStoreController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddAppStoreCommand addAppStoreCommand)
        {
            var results = await _mediator.Send(addAppStoreCommand, Request.HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpPost("getState")]
        public async Task<IActionResult> Post([FromBody]GetAppStoreCommand getAppStoreCommand)
        {
            var results = await _mediator.Send(getAppStoreCommand, Request.HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }
    }
}