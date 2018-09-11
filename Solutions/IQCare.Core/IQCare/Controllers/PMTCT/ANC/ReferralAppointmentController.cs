using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.ANC
{
   

    [Produces("application/json")]
    [Route("api/ReferralAppointment")]
    public class ReferralAppointmentController : Controller
    {
        private readonly IMediator _mediator;

        public ReferralAppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReferralAppointmentServiceCommand referralCommand)
        {
            PatientReferral referral_data = new PatientReferral()
            {
                PatientMasterVisitId= referralCommand.patientReferral.PatientMasterVisitId,
                ReferreFrom=referralCommand.patientReferral.ReferreFrom,
                ReferredTo=referralCommand.patientReferral.ReferredTo,
                ReferralReason=referralCommand.patientReferral.ReferralReason,
                ReferralDate=referralCommand.patientReferral.ReferralDate,
                ReferredBy=referralCommand.patientReferral.ReferredBy
            };

            PatientAppointment appointment_data = new PatientAppointment()
            {
                PatientMasterVisitId = referralCommand.patientAppointment.PatientMasterVisitId,
                PatientId = referralCommand.patientAppointment.PatientId,
                ServiceAreaId = referralCommand.patientAppointment.ServiceAreaId,
                AppointmentDate = referralCommand.patientAppointment.AppointmentDate,
                ReasonId = referralCommand.patientAppointment.ReasonId,
                Description = referralCommand.patientAppointment.Description,
                StatusDate = referralCommand.patientAppointment.StatusDate,
                StatusId = referralCommand.patientAppointment.StatusId
            };

            var response = await _mediator.Send(new ReferralAppointmentServiceCommand
            {
                patientReferral=referral_data,
                patientAppointment= appointment_data,
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
