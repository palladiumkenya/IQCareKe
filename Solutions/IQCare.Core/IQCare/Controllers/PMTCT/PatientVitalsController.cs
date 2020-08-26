using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.PMTCT
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PatientVitalsController : Controller
    {
        private readonly IMediator _mediator;

        public PatientVitalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<object> Add([FromBody] AddPatientVitalCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<object> Update([FromBody] UpdatePatientVitalCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }


        [HttpGet("{Id}")]
        public async Task<object> GetByPatientId(int id)
        {
            var response = await _mediator.Send(new GetPatientVitalsQuery {PatientId = id}, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }


        [HttpPost]
        public async Task<object> GetVitalsByVisitDate([FromBody] GetPatientVitalQueryByVisitDate getPatientVitalsQueryByVisitDate)
        {
            var response = await _mediator.Send(getPatientVitalsQueryByVisitDate, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }


        [HttpPost]
        public async Task<object> CalculateZscore([FromBody]CalculateZscoreCommand zscoreCommand)
        {
            var zscoreResult = await _mediator.Send(zscoreCommand, HttpContext.RequestAborted);

            if (zscoreResult.IsValid)
                return Ok(zscoreResult.Value);

            return BadRequest(zscoreResult);

        }
    }
}