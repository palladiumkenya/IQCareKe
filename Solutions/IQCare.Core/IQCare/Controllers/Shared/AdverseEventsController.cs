using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.AdverseEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdverseEventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdverseEventsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteAdverseEvents([FromBody] DeleteAdverseEventsCommand delAdverseEventCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(delAdverseEventCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdverseEvents([FromBody] AddAdverseEventCommand addAdverseEventCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addAdverseEventCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientAdverseEvents(int patientId)
        {
            var response = await _mediator.Send(new GetAdverseEventsCommand()
            {
                PatientId = patientId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}