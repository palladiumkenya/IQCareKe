using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Commands.Relationship;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost("addPatientOVCStatus")]
        public async Task<IActionResult> Post([FromBody] AddPatientOVCStatusCommand addPatientOVCStatusCommand)
        {
            var response = await _mediator.Send(addPatientOVCStatusCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPatientARVHistory/{serviceAreaId}/{personId}")]
        public async Task<IActionResult> GetPatientARVHistory(int serviceAreaId, int personId)
        {
            var response = await _mediator.Send(new GetPatientARVHistoryCommand()
            {
                PersonId = personId,
                ServiceId = serviceAreaId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("addPatientARVHistory")]
        public async Task<IActionResult> Post([FromBody] AddPatientARVHistoryCommand addarvhistorycommand)
        {
            var response = await _mediator.Send(addarvhistorycommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpGet("GetPatientTransferIn/{serviceAreaId}/{personId}")]
        public async Task<IActionResult> GetPatientTransfer(int serviceAreaId, int personId)
        {
            var response = await _mediator.Send(new GetPatientTransferInCommand()
            {
                PersonId = personId,
                ServiceId = serviceAreaId

            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("addPatientTransferIn")]
        public async Task<IActionResult> Post([FromBody] AddPatientTransferInCommand addtransferincommand)
        {
            var response = await _mediator.Send(addtransferincommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }



        [HttpGet("GetPatientOVCStatus/{personId}")]
        public async Task<IActionResult> GetPatientOVCStatus(int personId)
        {
            var response = await _mediator.Send(new GetPatientOVCStatusCommand()
            {
                PersonId = personId

            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("postServiceEntryPoint")]
        public async Task<IActionResult> PostServiceEntryPoint([FromBody]AddServiceEntryPointCommand serviceEntryPointCommand)
        {
            var response = await _mediator.Send(serviceEntryPointCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getServiceEntryPoint/{serviceAreaId}/{patientId}")]
        public async Task<IActionResult> GetServiceEntryPoint(int serviceAreaId, int patientId)
        {
            var response = await _mediator.Send(new GetServiceEntryPointCommand()
            {
                ServiceAreaId = serviceAreaId,
                PatientId = patientId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("postConfirmatoryTests")]
        public async Task<IActionResult> PostConfirmatoryTests([FromBody]AddHivReConfirmatoryTestsCommand addHivReConfirmatoryTestsCommand)
        {
            var response = await _mediator.Send(addHivReConfirmatoryTestsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPatientById/{patientId}")]
        public async Task<IActionResult> GetPatientById(int patientId)
        {
            var response = await _mediator.Send(new GetPatientByIdCommand()
            {
                Id = patientId
            }, Request.HttpContext.RequestAborted);
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

        [HttpPost("updatePersonContact")]
        public async Task<IActionResult> Post([FromBody]UpdatePersonContactCommand updatePersonContactCommand)
        {
            var response = await _mediator.Send(updatePersonContactCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("updatePersonMaritalStatus")]
        public async Task<IActionResult> Post([FromBody] UpdatePersonMaritalStatusCommand updatePersonMaritalStatusCommand)
        {
            var response = await _mediator.Send(updatePersonMaritalStatusCommand, Request.HttpContext.RequestAborted);
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
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("AddPatientReenrollment")]
        public async Task<IActionResult> AddPatientReenollment([FromBody] AddPatientReenrollmentCommand addPatientReenrollmentCommand)
        {
            var encountervisit = await _mediator.Send(addPatientReenrollmentCommand, Request.HttpContext.RequestAborted);
            if (encountervisit.IsValid)
                return Ok(encountervisit.Value);
            return BadRequest(encountervisit);
        }

        [HttpGet("GetPatientEnrollmentByServiceAreaId/{patientId}/{serviceAreaId}")]
        public async Task<IActionResult> GetPatientEnrollmentByServiceAreaId(int patientId, int serviceAreaId)
        {
            var response = await _mediator.Send(new GetPatientEnrollmentByServiceAreaIdCommand()
            {
                PatientId = patientId,
                ServiceAreaId = serviceAreaId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
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
            var response = await _mediator.Send(new GetClientDetailsCommand { PatientId = patientId, ServiceAreaId = serviceAreaId }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getEnrollmentMasterVisitId/{patientId}/{serviceAreaId}")]
        public async Task<IActionResult> GetEnrollmentMasterVisitId(int patientId,int serviceAreaId)
        {
            var response = await _mediator.Send(new GetEnrollmentMasterVisitCommand { PatientId = patientId, ServiceAreaId = serviceAreaId }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getPerson/{personId}")]
        public async Task<IActionResult> Get(int personId)
        {
            var response = await _mediator.Send(new GetPersonDetailsCommand	() {PersonId = personId});
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getPersonModel/{personId}")]
        public async Task<IActionResult> GetPersonModel(int personId)
        {
            var response = await _mediator.Send(new GetPersonQueryCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

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

        [HttpPost("UpdatePersonLocation")]
        public async Task<IActionResult> Post([FromBody]UpdatePersonLocationCommand updatePersonLocationCommand)
        {
            var response = await _mediator.Send(updatePersonLocationCommand, Request.HttpContext.RequestAborted);
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

        [HttpGet("GetPatientIdentifiers/{patientId}")]
        public async Task<IActionResult> GetPatientIdentifiers(int patientId)
        {
            var response = await _mediator.Send(new GetPatientIdentifiersCommand()
            {
                PatientId = patientId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPatientByPersonId/{personId}")]
        public async Task<IActionResult> GetPatientByPersonId(int personId)
        {
            var response = await _mediator.Send(new GetPatientByPersonIdCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}