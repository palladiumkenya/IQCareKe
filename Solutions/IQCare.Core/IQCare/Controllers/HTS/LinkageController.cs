using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/Linkage")]
    public class LinkageController : Controller
    {
        private readonly IMediator _mediator;

        public LinkageController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetPersonLinkage/{personId}")]
        public async Task<IActionResult> GetPersonLinkage(int personId)
        {
            var response = await _mediator.Send(new GetPersonLinkageCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}