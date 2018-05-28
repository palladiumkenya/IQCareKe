using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Commands.Partners;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;

namespace IQCare.Controllers.Registration
{
    [Produces("application/json")]
    [Route("api/Register")]
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterClientCommand registerClientCommand)
        {
            var response = await _mediator.Send(registerClientCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand updatePersonCommand)
        {
            var response = await _mediator.Send(updatePersonCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("addPatient")]
        public async Task<IActionResult> Post([FromBody] AddPatientCommand addPatientCommand)
        {
            var response = await _mediator.Send(addPatientCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("addPersonContact")]
        public async Task<IActionResult> Post([FromBody] AddPersonContactCommand addPersonContactCommand)
        {
            var response = await _mediator.Send(addPersonContactCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("addPersonMaritalStatus")]
        public async Task<IActionResult> Post([FromBody] AddPersonMaritalStatusCommand addPersonMaritalStatusCommand)
        {
            var response = await _mediator.Send(addPersonMaritalStatusCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("enrollment")]
        public async Task<IActionResult> Post([FromBody] EnrollClientCommand enrollClientCommand)
        {
            var response = await _mediator.Send(enrollClientCommand, Request.HttpContext.RequestAborted);
            if(response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get(string identificationNumber, string firstName, string middleName, string lastName)
        {
            var response = await _mediator.Send(new SearchEnrolledClientsCommand
            {
                identificationNumber = identificationNumber,
                firstName = firstName,
                middleName = middleName,
                lastName = lastName
            });

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int patientId, int serviceAreaId)
        {
            var response = await _mediator.Send(new GetClientDetailsCommand{ PatientId = patientId, ServiceAreaId = serviceAreaId }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getPerson/{personId}")]
        public async Task<IActionResult> Get(int personId)
        {
            var response = await _mediator.Send(new GetPartnerCommand() {PersonId = personId});
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("getPartners")]
        public async Task<IActionResult> GetPartners([FromBody]string[] relationshipTypes, int patientId)
        {
            var response = await _mediator.Send(new GetPartnersCommand { PatientId = patientId, RelationshipTypes = relationshipTypes}, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
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

        [HttpPost("AddPersonLocation")]
        public async Task<IActionResult> AddPersonLocation([FromBody]AddPersonLocationCommand addPersonLocationCommand)
        {
            var response = await _mediator.Send(addPersonLocationCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("AddPersonPopulationType")]
        public async Task<IActionResult> AddPersonPopulationType(
            [FromBody] AddPersonPopulationCommand addPersonPopulationCommand)
        {
            var response = await _mediator.Send(addPersonPopulationCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}