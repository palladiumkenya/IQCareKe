using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.BusinessProcess.Commands.Refferal;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using GetPatientAppointmentCommand = IQCare.PMTCT.BusinessProcess.Commands.Appointment.GetPatientAppointmentCommand;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.ANC
{
   

    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PatientReferralAndAppointmentController : Controller
    {
        private readonly IMediator _mediator;

        public PatientReferralAndAppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
             
        [HttpPost]
        public async Task<object> AddPatientNextAppointment([FromBody] AddPatientAppointmentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatientNextAppointment([FromBody] EditAppointmentCommand editAppointmentCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(editAppointmentCommand);
            var response = await _mediator.Send(editAppointmentCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetAppointment(int patientId, int patientMasterVisitId)
        {
            if (patientId < 1 || patientMasterVisitId < 1)
                return BadRequest(patientId);
            var response =
                await _mediator.Send(new GetPatientAppointmentCommand{PatientId = patientId, PatientMasterVisitId = patientMasterVisitId},
                    HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetReferral(int patientId, int patientMasterVisitId)
        {
            if (patientId < 1 || patientMasterVisitId < 1)
                return BadRequest(patientId);

            var response = await _mediator.Send(new PmtctReferralCommand{ PatientId = patientId, PatientMasterVisitId = patientMasterVisitId },HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpPost]
        public async Task<object> AddPatientReferralInfo([FromBody] AddPatientReferralCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatientReferralInfo([FromBody]EditRefferalCommand editRefferalCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(editRefferalCommand);

            var response = await _mediator.Send(editRefferalCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetPatientAppoitment(int Id)
        {
            var results = await _mediator.Send(new GetPatientAppointmentViewQuery() { PatientId = Id }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

    }
}
