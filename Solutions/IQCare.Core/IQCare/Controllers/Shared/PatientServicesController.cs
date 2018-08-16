using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
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

        [HttpGet("GetContactByPersonId/{personId}")]
        public async Task<IActionResult> GetContactByPersonId(int personId)
        {
            var results = await _mediator.Send(new GetPersonContactViewCommand { personId = personId },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
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
