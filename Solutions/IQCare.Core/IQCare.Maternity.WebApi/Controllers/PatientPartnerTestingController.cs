using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[Action]")]
    public class PatientPartnerTestingController : Controller
    {
        IMediator _mediator;
        public PatientPartnerTestingController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> AddPartnerTesting([FromBody] PatientPartnerTestingCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]UpdatePatientPartnerTestingCommand updatePatientPartnerTestingCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(updatePatientPartnerTestingCommand);

            var response = await _mediator.Send(updatePatientPartnerTestingCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}")]
        public async Task<object> Get(int patientId)
        {
            var patientPartnerTesting = await _mediator.Send(new GetPatientPartnerTestingQuery { PatientId = patientId }, HttpContext.RequestAborted);
            if (patientPartnerTesting.IsValid)
                return Ok(patientPartnerTesting.Value);
            return BadRequest(patientPartnerTesting);
        }
    }
}