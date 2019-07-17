using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PregnancyIndicatorController : ControllerBase
    {
        IMediator _mediator;

        public PregnancyIndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPregnancyIndicator([FromBody]AddPregnancyIndicatorCommand addPregnancyIndicatorCommand)
        {
            var result = await _mediator.Send(addPregnancyIndicatorCommand, Request.HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);
            return BadRequest(result);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetPregnancyIndicator(int patientId, int patientMasterVisitId)
        {
            var result = await _mediator.Send(new GetPregnancyIndicatorCommand()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId
            }, Request.HttpContext.RequestAborted);

            if (result.IsValid)
                return Ok(result.Value);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPregnancyOutcome([FromBody]AddPregnancyLogCommand addPregnancyLogCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addPregnancyLogCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientMastervisitId}")]
        public async Task<IActionResult> GetPregnancyOutcome(int patientId, int patientMastervisitId)
        {
            var response = await _mediator.Send(new GetPregnancyLogCommand()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMastervisitId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}