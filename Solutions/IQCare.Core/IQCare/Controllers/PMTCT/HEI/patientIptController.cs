using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIpt;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiIpt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/PatientIpt")]
    public class PatientIptController : Controller
    {
        private readonly IMediator _mediator;

        public PatientIptController(IMediator mediator)
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
        [HttpGet("GetIpt")]
        public async Task<IActionResult> GetIpt([FromBody] GetHeiPatientIptCommand getHeiPatientIptCommand)
        {
            var response = await _mediator.Send(getHeiPatientIptCommand, Request.HttpContext.RequestAborted);
            if (response)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddHeiPatientIptCommand addHeiPatientIptCommand )
        {
            var response = await _mediator.Send(addHeiPatientIptCommand, Request.HttpContext.RequestAborted);
            if (response)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditHeiPatientIptCommand editHeiPatientIptCommand)
        {
            var response = await _mediator.Send(editHeiPatientIptCommand, Request.HttpContext.RequestAborted);
            if (response)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
