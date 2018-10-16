using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/tbAssessment")]
    public class TbAssessmentController : Controller
    {
        private readonly IMediator _mediator;

        public TbAssessmentController(IMediator mediator)
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
        [HttpGet("patientIcf")]
        public async Task<IActionResult> GetIcf([FromBody] GetPatientIcfCommand getPatientIcfCommand )
        {
            var response = await _mediator.Send(getPatientIcfCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        //Get PatientICFAction
        [HttpGet("patientIcfAction")]
        public async Task<IActionResult> GetIcfAction([FromBody] GetHeiPatientIcfActionCommand getPatientIcfActionCommand)
        {
            var response = await _mediator.Send(getPatientIcfActionCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> AddPatientIcf([FromBody] AddPatientIcfCommand addPatientIcfCommand)
        {
            var response = await _mediator.Send(addPatientIcfCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> AddPatientIcfAction([FromBody] AddPatientIcfActionCommand addPatientIcfActionCommand)
        {
            var response = await _mediator.Send(addPatientIcfActionCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // PUT api/<controller>/5
        [HttpPut("EditPatientIcf")]
        public async Task<IActionResult> EditPatientIcf([FromBody] EditPatientIcfCommand editPatientIcf)
        {
            var response = await _mediator.Send(editPatientIcf, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut("EditPatientIcfAction")]
        public async Task<IActionResult> EditPatientIcf([FromBody] EditHeiPatientIcfActionCommand editHeiPatientIcfActionCommand)
        {
            var response = await _mediator.Send(editHeiPatientIcfActionCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
