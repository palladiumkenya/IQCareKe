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
        public async Task<object> AddPatientReferralInfo([FromBody] AddPatientReferralCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

    }
}
