using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
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

        [HttpPost("addTestResults")]
        public async Task<IActionResult> Post([FromBody]AddTestingCommand addTestingCommand)
        {
            var consentList = new List<KeyValuePair<string, int>>();
            //consentList.Add(new KeyValuePair<string, int>("ConsentToBeTested", addEncounterCommand.Encounter.Consent));
            consentList.Add(new KeyValuePair<string, int>("ConsentToListPartners", addTestingCommand.FinalTestingResult.AcceptedPartnerListing));

            var consent = await _mediator.Send(new AddConsentCommand()
            {
                PatientID = addTestingCommand.PatientId,
                PatientMasterVisitId = addTestingCommand.PatientMasterVisitId,
                ConsentDate = DateTime.Now,
                ConsentType = consentList,
                ServiceAreaId = addTestingCommand.ServiceAreaId,
                DeclineReason = addTestingCommand.FinalTestingResult.ReasonsDeclinePartnerListing,
                UserId = addTestingCommand.ProviderId
            }, Request.HttpContext.RequestAborted);

            if (consent.IsValid)
            {

            }
            else
            {
                return BadRequest(consent);
            }

            var response = await _mediator.Send(addTestingCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getTestResults/{patientMasterVisitId}/{patientEncounterId}")]
        public async Task<IActionResult> Get(int patientMasterVisitId, int patientEncounterId)
        {
            var response = await _mediator.Send(new GetTestingCommand()
            {
                PatientMasterVisitId = patientMasterVisitId,
                PatientEncounterId = patientEncounterId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEncounterCommand addEncounterCommand)
        {
            int patientMasterVisitId = 0;
            int patientEncounterId = 0;
            if (addEncounterCommand.Encounter.PatientEncounterID == 0)
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
                    patientEncounterId = encounter.Value.PatientEncounterId;
                    addEncounterCommand.Encounter.PatientEncounterID = patientEncounterId;
                    patientMasterVisitId = encounter.Value.PatientMasterVisitId;
                }
                else
                {
                    return BadRequest(encounter);
                }
            }
            else
            {
                patientMasterVisitId = addEncounterCommand.Encounter.PatientMasterVisitId;
                patientEncounterId = addEncounterCommand.Encounter.PatientEncounterID;
                addEncounterCommand.Encounter.PatientEncounterID = patientEncounterId;
            }
            

            var consentList = new List<KeyValuePair<string, int>>();
            consentList.Add(new KeyValuePair<string, int>("ConsentToBeTested", addEncounterCommand.Encounter.Consent));

            var consent = await _mediator.Send(new AddConsentCommand()
            {
                PatientID = addEncounterCommand.Encounter.PatientId,
                PatientMasterVisitId = patientMasterVisitId,
                ConsentDate = addEncounterCommand.Encounter.EncounterDate,
                ConsentType = consentList,
                ServiceAreaId = addEncounterCommand.Encounter.ServiceAreaId,
                UserId = addEncounterCommand.Encounter.ProviderId
            }, Request.HttpContext.RequestAborted);

            if (consent.IsValid)
            {

            }
            else
            {
                return BadRequest(consent);
            }

            if (addEncounterCommand.Encounter.TbScreening.HasValue)
            {
                var screeningList = new List<KeyValuePair<string, int>>();
                screeningList.Add(new KeyValuePair<string, int>("TbScreening", addEncounterCommand.Encounter.TbScreening.Value));

                var tbScreening = await _mediator.Send(new AddPatientScreeningCommand()
                {
                    PatientId = addEncounterCommand.Encounter.PatientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    ScreeningDate = addEncounterCommand.Encounter.EncounterDate,
                    UserId = addEncounterCommand.Encounter.ProviderId,
                    PersonId = addEncounterCommand.Encounter.PersonId,
                    ScreeningType = screeningList
                }, Request.HttpContext.RequestAborted);

                if (tbScreening.IsValid)
                {

                }
                else
                {
                    return BadRequest(tbScreening);
                }
            }
            

            var response = await _mediator.Send(addEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
            {
                response.Value.PatientMasterVisitId = patientMasterVisitId;
                return Ok(response.Value);
            }

            return BadRequest(response);
        }

        [HttpPut("updateEncounter/{encounterId}/{patientMasterVisitId}")]
        public async Task<IActionResult> Update(int encounterId, int patientMasterVisitId, [FromBody] UpdateEncounterCommand updateEncounterCommand)
        {
            updateEncounterCommand.encounterId = encounterId;

            if (updateEncounterCommand.Encounter.TbScreening.HasValue)
            {
                var screeningList = new List<KeyValuePair<string, int>>();
                screeningList.Add(new KeyValuePair<string, int>("TbScreening", updateEncounterCommand.Encounter.TbScreening.Value));

                var result = await _mediator.Send(new UpdatePatientScreeningCommand()
                {
                    ScreeningType = screeningList,
                    PatientMasterVisitId = patientMasterVisitId,
                    PatientId = updateEncounterCommand.Encounter.PatientId,
                    UserId = updateEncounterCommand.Encounter.ProviderId,
                    ScreeningDate = updateEncounterCommand.Encounter.EncounterDate
                }, Request.HttpContext.RequestAborted);

                if (result.IsValid)
                {

                }
                else
                {
                    return BadRequest(result);
                }
            }

            var response = await _mediator.Send(updateEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getEncounter/{encounterId}")]
        public async Task<IActionResult> GetEncounter(int encounterId)
        {
            var response = await _mediator.Send(new GetEncounterCommand()
            {
                EncounterId = encounterId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
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

        [HttpPost("tracing")]
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

        [HttpGet("lastHtsEncounters/{PersonId}")]
        public async Task<IActionResult> GetLastEncounter(int PersonId)
        {
            var response = await _mediator.Send(new GetPersonLastHtsEncounterCommand()
            {
                PersonId = PersonId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getEncounterDetails/{encounterId}")]
        public async Task<IActionResult> GetEncounterDetails(int encounterId)
        {
            var response = await _mediator.Send(new GetHtsEncounterDetailsViewCommand()
            {
                EncounterId = encounterId
            }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("getPsmartData/{personId}")]
        public async Task<IActionResult> GetPsmartData(int personId)
        {
            var response = await _mediator.Send(new GetClientPsmartDataCommand()
            {
                PersonId = personId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}
