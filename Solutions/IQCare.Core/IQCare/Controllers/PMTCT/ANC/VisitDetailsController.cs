using System;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.BusinessProcess.Commands.PatientMasterVisit;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/VisitDetails")]
    public class VisitDetailsController : Controller
    {
        private readonly IMediator _mediator;

        public VisitDetailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VisitDetailsCommand visitDetailsCommand )
        {
            var response = await _mediator.Send(new VisitDetailsCommand
            {
                PatientId = visitDetailsCommand.PatientId,
                ServiceAreaId = visitDetailsCommand.ServiceAreaId,
                VisitDate = visitDetailsCommand.VisitDate,
                VisitType = visitDetailsCommand.VisitType,
                VisitNumber= visitDetailsCommand.VisitNumber,
                Lmp=visitDetailsCommand.Lmp ,
                Edd= visitDetailsCommand.Edd,
                Gestation=visitDetailsCommand.Gestation,
                AgeAtMenarche= visitDetailsCommand.AgeAtMenarche ,
                ParityOne=visitDetailsCommand.ParityOne,
                ParityTwo=visitDetailsCommand.ParityTwo,
                Gravidae= visitDetailsCommand.Gravidae ,
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);

        }

        [HttpGet("GetAncProfile/{patientId}/{pregnancyId}")]
        public async Task<IActionResult>GetANcProfile(int patientId,int pregnancyId)
        {
            var results = await _mediator.Send(new GetANCInitialProfileCommand() { PatientId = patientId,PregnancyId = pregnancyId}, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetPregnancyProfile/{patientId}")]
        public async Task<IActionResult> GetPregnancyProfile(int patientId)
        {
            var results = await _mediator.Send(new GetPregnancyCommand() { PatientId = patientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }
    }
}