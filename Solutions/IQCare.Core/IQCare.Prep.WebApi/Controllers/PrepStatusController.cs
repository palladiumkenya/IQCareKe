using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Prep.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Prep.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrepStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrepStatusController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<IActionResult> AddPrepStatus([FromBody]AddPrepStatusCommand addPrepStatusCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addPrepStatusCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientEncounterId}")]
        public async Task<IActionResult> GetPrepStatus(int patientId, int patientEncounterId)
        {
            var response = await _mediator.Send(new GetPrepStatusCommand()
            {
                PatientId = patientId,
                PatientEncounterId = patientEncounterId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }



        [HttpGet("{patientId}/{startitemId}")]
        public async Task<IActionResult> GetPrepStartEventStatus(int patientId, int startitemId)
        {
            var response = await _mediator.Send(new GetPrepStatusDateEventCommand()
            {
                PatientId = patientId,
                startitemId =startitemId    
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}