using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/Hei")]
    public class HeiController: Controller
    {
        private readonly IMediator _mediator;

        public HeiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddHeiFeedingCommand heiFeedingCommand)
        {
            var response = await _mediator.Send(new AddHeiFeedingCommand
            {
                PatientMasterVisitId = heiFeedingCommand.PatientMasterVisitId,
                PatientId = heiFeedingCommand.PatientId,
                FeedingModeId = heiFeedingCommand.FeedingModeId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpGet("GetHeiFeeding/{patientId}")]
        public async Task<IActionResult> GetHeiFeeding(int patientId)
        {
            var results = await _mediator.Send(new GetHeiFeedingCommand() { PatientId = patientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetHeiFeeding/{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetHeiFeeding(int patientId,int patientMasterVisitId)
        {
            var results = await _mediator.Send(new GetHeiFeedingCommand() { PatientId = patientId, PatientMasterVisitId = patientMasterVisitId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }
    }
}
