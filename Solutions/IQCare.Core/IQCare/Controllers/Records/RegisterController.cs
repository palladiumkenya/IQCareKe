using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IQCareRecords.Common.BusinessProcess.Command;
 using MediatR;
using IQCare.Common.BusinessProcess.Commands.Partners;
using IQCare.Records.BusinessProcess.Command;
using IQCare.Records.BusinessProcess.Command.Lookup;
using IQCare.Records.BusinessProcess.Command.Registration;

namespace IQCare.Controllers.Records
{
    [Produces("application/json")]
    [Route("records/api/Register")]
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

        [HttpPost("addPersonEducationalLevel")]
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

        [HttpPost("addPatientRegistration")]
        public async Task<IActionResult> Post([FromBody] AddRegistrationPatientCommand registerpatientcommand)
        {
            var response = await _mediator.Send(registerpatientcommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }
       
         

        [HttpPost("addPersonLocation")]
        public async Task<IActionResult> Post([FromBody] AddUpdatePersonLocationCommand registerpersonlocationcommand)
        {
            var response = await _mediator.Send(registerpersonlocationcommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPost("addPersonMaritalStatus")]
        public async Task<IActionResult> Post([FromBody] AddPersonMaritalStatusCommand personMaritalStatusCommand)
        {
            var response = await _mediator.Send(personMaritalStatusCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpPost("addPersonEmergencyContact")]
        public async Task<IActionResult> Post([FromBody] PersonEmergencyContactCommand personEmergencyContactCommand)
        {
            var response = await _mediator.Send(personEmergencyContactCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

       [HttpPost("AddPersonContact")]
        public async Task<IActionResult> Post([FromBody] AddUpdatePersonContactCommand addUpdatePersonContactCommand)
        {
            var response = await _mediator.Send(addUpdatePersonContactCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }
        [HttpGet("searchpersonlist")]
        public async Task<IActionResult> Get([FromQuery]SearchQuery searchQuery)
        {
            var response = await _mediator.Send(new SearchPersonListCommand
            {
                identificationNumber = searchQuery.IdentifierValue,
                firstName = searchQuery.FirstName,
                middleName = searchQuery.MiddleName,
                lastName = searchQuery.LastName,
                MobileNumber = searchQuery.MobileNumber
            });

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpGet("search")]
        public async Task<IActionResult> Get(string identificationNumber, string firstName, string middleName, string lastName,string enrollmentNumber,Boolean notClient)
        {
            var response = await _mediator.Send(new SearchPersonCommand
            {
                identificationNumber = identificationNumber,
                firstName = firstName,
                middleName = middleName,
                lastName = lastName,
                EnrollmentNumber = enrollmentNumber,
                NotClient = notClient
                
            });

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
        [HttpGet("SearchContact")]
        public async Task<IActionResult> GetContacts(string identificationNumber, string firstName, string middleName, string lastName, string enrollmentNumber)
        {
            var response = await _mediator.Send(new SearchPersonContactListCommand
            {
                identificationNumber = identificationNumber,
                firstName = firstName,
                middleName = middleName,
                lastName = lastName,
                EnrollmentNumber = enrollmentNumber
                

            });

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPersonDetails/{personId}")]
        public async Task<IActionResult> GetPersonDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonDetailsCommand
            {
                PersonId=personId
            });
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);

        }

        [HttpGet("GetPersonKinContacts/{personId}")]
        public async Task<IActionResult> GetPersonKinContacts(int personId)
        {
            var response = await _mediator.Send(new GetPersonKinContactsCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}