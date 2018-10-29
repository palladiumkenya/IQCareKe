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

        //used to capture both maternity and ANC visit details
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VisitDetailsCommand visitDetailsCommand )
        {
            var response = await _mediator.Send(visitDetailsCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);

        }

        [HttpGet("GetAncProfile/{patientId}/{pregnancyId}")]
        public async Task<IActionResult>GetANcProfile(int patientId,int pregnancyId)
        {
            var results = await _mediator.Send(new GetPatientInitialProfileCommand() { PatientId = patientId,PregnancyId = pregnancyId}, HttpContext.RequestAborted);
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
        
        // Used to capture PNC visit details
        
        [HttpPost]
        public async Task<IActionResult> AddPNCVisitDetails([FromBody] AddPNCVisitCommand command)
        {
            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        //Used to get patient profile on PNC 
        [HttpGet("GetInitialProfileDetailsByPatientId/{Id}")]
        public async Task<IActionResult> GetInitialProfileDetailsByPatientId(int Id)
        {
            var results = await _mediator.Send(new GetPatientInitialProfileCommand() { PatientId = Id }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

    }
}