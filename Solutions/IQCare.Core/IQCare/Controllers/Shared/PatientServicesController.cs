using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Commands.PersonVitals;
using IQCare.Common.BusinessProcess.Commands.Relationship;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/PatientServices")]
    public class PatientServicesController : Controller
    {
        private readonly IMediator _mediator;

        public PatientServicesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/PatientServices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PatientServices/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        //GET: api/PersonServices/getPerson/{id}
        [HttpGet("GetPatientByPersonId/{personId}")]
        public async Task<IActionResult> GetPatientByPersonId(int personId)
        {
            var results = await _mediator.Send(new GetPersonDetailsCommand { PersonId = personId },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        //GET: api/PersonServices/getPerson/{id}
        [HttpGet("GetLocationByPersonId/{personId}")]
        public async Task<IActionResult> GetLocationByPersonId(int personId)
        {
            var results = await _mediator.Send(new GetPersonLocationViewCommand { personId = personId },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }
        [HttpGet("GetMasterVisits/{PatientId}/{PatientMasterVisitId}")]
        public async Task<IActionResult> GetMasterVisits(int PatientId, int PatientMasterVisitId)
        {
         var results = await _mediator.Send(new GetPatientMasterVisitCommand { PatientId = PatientId, PatientMasterVisitId = PatientMasterVisitId },
            HttpContext.RequestAborted);

         if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

[HttpGet("GetEncounters/{PatientId}/{encounterTypeId}")]
        public async Task<IActionResult> GetEncounters(int PatientId,int encounterTypeId)
        {
            var results = await _mediator.Send(new GetPatientEncounterCommand { PatientId = PatientId, EncounterTypeId = encounterTypeId},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("GetContactByPersonId/{personId}")]
        public async Task<IActionResult> GetContactByPersonId(int personId)
        {
            var results = await _mediator.Send(new GetPersonContactViewCommand { personId = personId },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("GetEnrolledServicesByPersonId/{personId}")]
        public async Task<IActionResult> GetEnrolledServicesByPersonId(int personId)
        {
            var results = await _mediator.Send(new GetEnrolledServicesCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetRelationshipsByPatientId/{patientId}")]
        public async Task<object> GetRelationshipsByPatientId(int patientId)
        {
            var response = await _mediator.Send(new GetPatientRelationships { PatientId = patientId }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetCurrentPersonVitals/{Id}")]
        public async Task<Object> GetCurrentPersonVitals(int id)
        {
            var response = await _mediator.Send(new GetPersonVitalsCommand { PersonId = id }, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpPost("CareEndPatient")]
        public async Task<IActionResult> CareEndPatient([FromBody] AddPatientCareEndingCommand addPatientCareEndingCommand)
        {
            var response = await _mediator.Send(addPatientCareEndingCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpGet("GetPatientCareEndedDetails/{patientMasterVisitId}")]
        public async  Task<IActionResult> GetPatientCareEndedDetails(int patientMasterVisitId)
        {
            var response = await _mediator.Send(new GetPatientCareEndingCommand { PatientMasterVisitId = patientMasterVisitId }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);

        }

        [HttpGet("GetLatestCareEndDetails/{patientId}")]

        public async Task<IActionResult> GetLatestCareEndDetails(int patientId)
        {
            var response = await _mediator.Send(new GetLatestCareEndingDetailsCommand { PatientId = patientId }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

       
        // POST: api/PatientServices
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/PatientServices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
