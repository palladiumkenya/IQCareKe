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

        [HttpGet]
        public async Task<object> Get(int Id)
        {
            var patientPartnerTesting = await _mediator.Send(new GetPatientPartnerTestingQuery { PatientId = Id }, HttpContext.RequestAborted);
            if (patientPartnerTesting.IsValid)
                return Ok(patientPartnerTesting.Value);
            return BadRequest(patientPartnerTesting);
        }
    }
}