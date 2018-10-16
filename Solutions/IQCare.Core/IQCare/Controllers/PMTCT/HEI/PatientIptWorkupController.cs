using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatientIptWorkupController : Controller
    {
        private readonly IMediator _mediator;

        public PatientIptWorkupController(IMediator mediator)
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
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] GetHeiIptWorkupCommand getHeiIptWorkupCommand)
        {
            var response = await _mediator.Send(getHeiIptWorkupCommand, Request.HttpContext.RequestAborted);
            if (response)
                return Ok(response.Value);
            return BadRequest(response.Value);
            ;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddHeiPatientIptWorkupCommand addHeiPatientIptWorkupCommand)
        {
            var response = await _mediator.Send(addHeiPatientIptWorkupCommand, Request.HttpContext.RequestAborted);
            if (response)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditHeiPatientIptWorkupCommand editHeiPatientIptWorkupCommand)
        {
            var response = await _mediator.Send(editHeiPatientIptWorkupCommand, Request.HttpContext.RequestAborted);
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
