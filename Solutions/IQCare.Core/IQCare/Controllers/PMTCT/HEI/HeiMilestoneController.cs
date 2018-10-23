using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/HeiMilestone")]
    public class HeiMilestoneController : Controller
    {
        private readonly IMediator _mediator;

        public HeiMilestoneController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetPatientMilestoneCommand getPatientMilestone)
        {
            var response = await _mediator.Send(getPatientMilestone, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddMilestoneCommand addMilestoneCommand)
        {
            var response = await _mediator.Send(addMilestoneCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]EditMilestoneCommand editMilestoneCommand)
        {
            var response = await _mediator.Send(editMilestoneCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
