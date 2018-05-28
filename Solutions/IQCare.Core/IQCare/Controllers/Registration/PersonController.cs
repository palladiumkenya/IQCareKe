using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Registration
{
    [Produces("application/json")]
    [Route("api/Registration/Person")]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("getPersonDetails/{personId}")]
        public async Task<IActionResult> GetPersonDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonDetailsCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpGet("GetPersonPopulationDetails/{personId}")]
        public async Task<IActionResult> GetPersonPopulationDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonPopulationCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPersonPriorityDetails/{personId}")]
        public async Task<IActionResult> GetPersonPriorityDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonPriorityDetailsCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}