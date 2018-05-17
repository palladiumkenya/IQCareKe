using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IQCareRecords.Common.BusinessProcess.Commands;
using MediatR;
using IQCare.Common.BusinessProcess.Commands.Partners;

namespace IQCare.Controllers.Records
{
    [Produces("application/json")]
    [Route("Records/api/Register")]
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController (IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost("addPerson")]
        public async Task<IActionResult> Post([FromBody] PersonRegistrationCommand registerPersonCommand)
        {
            var response = await _mediator.Send(registerPersonCommand, Request.HttpContext.RequestAborted);
            if(response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
            
        }

        [HttpPost("addEducationLevel")]
        public async Task<IActionResult> Post([FromBody] PersonEducationLevelCommand registerEducationCommand)
        {
            var response = await _mediator.Send(registerEducationCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response); 
        }
        [HttpPost("addPersonOccupation")]
        public async  Task<IActionResult> Post([FromBody] PersonOccupationLevelCommand registeroccupationcommand)
        {
            var response = await _mediator.Send(registeroccupationcommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("addPersonIdentifier")]
        public async Task<IActionResult> Post([FromBody]  PersonIdentifierCommand registerIdentifiercommand )
        {
            var response = await _mediator.Send(registerIdentifiercommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("AddPersonLocation")]
        public async Task<IActionResult> Post([FromBody] AddUpdatePersonLocationCommand registerpersonlocationcommand)
        {
            var response = await _mediator.Send(registerpersonlocationcommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("AddPersonEmergencyContact")]
        public async Task<IActionResult> Post([FromBody] PersonEmergencyContactCommand personEmergencyContactCommand)
        {
            var response = await _mediator.Send(personEmergencyContactCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("addPersonRelationship")]
        public async Task<IActionResult> AddPersonRelationship(
           [FromBody] AddPersonRelationshipCommand addPersonRelationshipCommand)
        {
            var response = await _mediator.Send(addPersonRelationshipCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
       
    }
}