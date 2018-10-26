using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.PNCCommands;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using IQCare.Maternity.Core;


namespace IQCareMaternityWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MartenityPatientVisitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MartenityPatientVisitController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientVisitDetails([FromBody] VisitDetailsCommand visitDetailsCommand)
        {
            var response = await _mediator.Send(visitDetailsCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpGet("GetAncProfile/{patientId}/{pregnancyId}")]
        public async Task<IActionResult> GetANcProfile(int patientId, int pregnancyId)
        {
            var results = await _mediator.Send(new GetANCInitialProfileCommand() { PatientId = patientId, PregnancyId = pregnancyId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetPregnancyProfile/{patientId}")]
        public async Task<IActionResult> GetPregnancyProfile(int patientId)
        {
            var results = await _mediator.Send(new GetANCInitialProfileCommand() { PatientId = patientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("GetMaternityProfile/{patientId}")]
        public async Task<IActionResult> GetMaternityProfile(int patientId)
        {
            var results = await _mediator.Send(new GetMaternetyAndPncProfileCommand() { PatientId = patientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPNCProfile(int patientId)
        {
            var results = await _mediator.Send(new GetMaternetyAndPncProfileCommand() { PatientId = patientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }


    }
}