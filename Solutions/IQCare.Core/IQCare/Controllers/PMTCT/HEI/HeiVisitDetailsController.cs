using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiProfile;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Route("api/[controller]")]
    [Route("api/HeiVisitDetails")]

    public class HeiVisitDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public HeiVisitDetailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/<controller>
        [HttpGet("{patientId}")]
        public async Task<IActionResult> Get(GetHeiProfileCommand PatientId)
        {
            var response =await  _mediator.Send(PatientId, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }
    

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProfileCommand addProfileCommand)
        {
            var response = await _mediator.Send(addProfileCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]EditProfileCommand editProfileCommand)
        {
            var response = await _mediator.Send(editProfileCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DeleteProfileCommand deleteProfileCommand)
        {
            var response = await _mediator.Send(deleteProfileCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
