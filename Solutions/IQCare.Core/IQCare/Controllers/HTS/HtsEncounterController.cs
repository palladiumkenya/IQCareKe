using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Consent;

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
                UserId = addEncounterCommand.Encounter.ProviderId,
                EncounterDate = addEncounterCommand.Encounter.EncounterDate
            }, Request.HttpContext.RequestAborted);

            if (encounter.IsValid)
            {
                addEncounterCommand.Encounter.PatientEncounterID = encounter.Value.PatientEncounterId;
            }
            else
            {
                return BadRequest(encounter);
            }

            var consentList = new List<KeyValuePair<string, int>>();
            consentList.Add(new KeyValuePair<string, int>("ConsentToBeTested", addEncounterCommand.Encounter.Consent));
            consentList.Add(new KeyValuePair<string, int>("ConsentToListPartners", addEncounterCommand.FinalTestingResult.AcceptedPartnerListing));

            var consent = await _mediator.Send(new AddConsentCommand()
            {
                PatientID = addEncounterCommand.Encounter.PatientId,
                PatientMasterVisitId = encounter.Value.PatientMasterVisitId,
                ConsentDate = addEncounterCommand.Encounter.EncounterDate,
                ConsentType = consentList,
                ServiceAreaId = addEncounterCommand.Encounter.ServiceAreaId,
                DeclineReason = addEncounterCommand.FinalTestingResult.ReasonsDeclinePartnerListing.ToString(),
                UserId = addEncounterCommand.Encounter.ProviderId
            }, Request.HttpContext.RequestAborted);

            if (consent.IsValid)
            {
                
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

        [HttpPost("pnsScreening")]
        public async Task<IActionResult> Post([FromBody] PatientScreeningCommand addPnsScreeningCommand)
        {
            var response = await _mediator.Send(addPnsScreeningCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("pnsTracing")]
        public async Task<IActionResult> Post([FromBody]AddPnsTracingCommand addPnsTracingCommand)
        {
            var response = await _mediator.Send(addPnsTracingCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{PatientId}")]
        public async Task<IActionResult> Get(int PatientId)
        {
            var response = await _mediator.Send(new GetHtsEncountersCommand()
            {
                PatientId = PatientId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}
