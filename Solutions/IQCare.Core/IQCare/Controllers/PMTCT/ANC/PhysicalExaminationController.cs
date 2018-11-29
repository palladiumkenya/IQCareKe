using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PhysicalExaminationController : Controller
    {
        private readonly IMediator _mediator;

        public PhysicalExaminationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{patientId}/{PatientMasterVisitId}")]
        public async Task<IActionResult> GetPhysicalExam(int patientId, int patientMasterVisitId)
        {
            var response = await 
                _mediator.Send(
                    new GetPhysicalExaminationViewCommand
                        {PatientMasterVisitId = patientMasterVisitId, PatientId = patientId},
                    HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
