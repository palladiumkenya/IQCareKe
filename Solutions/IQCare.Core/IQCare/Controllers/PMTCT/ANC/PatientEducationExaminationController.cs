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
    [Route("api/PatientEducationExamination")]
    public class PatientEducationExaminationController : Controller
    {
        private readonly IMediator _mediator;

        public PatientEducationExaminationController(IMediator mediator)
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientEducationExaminationCommand serviceCommand)
        {
            var response = await _mediator.Send(new PatientEducationExaminationCommand
            {
               // PatientId = serviceCommand.PatientId,
                PatientId = 5,
                //PatientMasterVisitId= serviceCommand.PatientMasterVisitId,
                PatientMasterVisitId = 21,
                BreastExamDone= serviceCommand.BreastExamDone,
                TreatedSyphilis= serviceCommand.TreatedSyphilis,
                CounsellingTopics= serviceCommand.CounsellingTopics,
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
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
