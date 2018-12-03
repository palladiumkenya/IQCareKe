using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/ImmunizationHistory")]

    public class ImmunizationHistoryController : Controller
    {
        private readonly IMediator _mediator;

        public ImmunizationHistoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/<controller>
        [HttpGet("{patientId}")]
        public async Task<IActionResult> Get(GetImmunizationHistoryCommand immunizationHistoryCommand)
        {
            var response = await _mediator.Send(immunizationHistoryCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        // GET api/<controller>/5
        [HttpGet]
        public void Get(int id)
        {
            
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] AddImmunizationHistoryCommand addImmunizationHistoryCommand)
        {
            var response = await _mediator.Send(addImmunizationHistoryCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditImmunizationHistoryCommand editImmunizationHistoryCommand)
        {
            var response = await _mediator.Send(editImmunizationHistoryCommand, Request.HttpContext.RequestAborted);
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
