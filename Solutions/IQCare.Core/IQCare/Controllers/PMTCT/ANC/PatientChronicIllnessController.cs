using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness;
using IQCare.PMTCT.BusinessProcess.Queries;
using IQCare.PMTCT.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PatientChronicIllnessController : Controller
    {
        readonly IMediator _mediator;
        public PatientChronicIllnessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<object> GetByPatientId(int Id)
        {
            var response = await _mediator.Send(new GetPatientChronicIllnessInfo { PatientId = Id }, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPatientChronicIllnessCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditPatientChronicIllnessCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id <= 0)
                return BadRequest(Id);
            var response = await _mediator.Send(new DeletePatientChronicIllnessCommand {Id=Id}, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

    }
}