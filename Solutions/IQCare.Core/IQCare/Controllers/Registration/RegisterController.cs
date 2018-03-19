using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;

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
    }
}