using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/HtsEncounter")]
    public class HtsEncounterController : Controller
    {
        private readonly IMediator _mediator;

        public HtsEncounterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEncounterCommand addEncounterCommand)
        {
            var encounter = await _mediator.Send(new AddEncounterVisitCommand
            {
                PatientId = addEncounterCommand.Encounter.PatientId,
                ServiceAreaId = addEncounterCommand.Encounter.ServiceAreaId,
                EncounterType = addEncounterCommand.Encounter.EncounterTypeId,
                UserId = addEncounterCommand.Encounter.ProviderId
            }, Request.HttpContext.RequestAborted);

            if (encounter.IsValid)
            {
                addEncounterCommand.Encounter.EncounterTypeId = encounter.Value.PatientEncounterId;
            }
            else
            {
                return BadRequest(encounter);
            }
            

            var response = await _mediator.Send(addEncounterCommand, Request.HttpContext.RequestAborted);
            if(response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPnsScreeningCommand addPnsScreeningCommand)
        {
            var response = await _mediator.Send(addPnsScreeningCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}
