using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PatientEducationExaminationController : Controller
    {
        private readonly IMediator _mediator;

        public PatientEducationExaminationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetPatientEducation(int patientId, int patientMasterVisitId)
        {
            var response= await _mediator.Send(new GetPatientEducationCommand { PatientMasterVisitId = patientMasterVisitId, PatientId = patientId }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{PatientId}/{PatientMasterVisitId}")]
        public async Task<IActionResult> GetPatientCounseling(int patientId, int patientMasterVisitId)
        {
            var response =
                await _mediator.Send(new GetPatientCounselingViewCommand { PatientId = patientId, PatientMasterVisitId = patientMasterVisitId},
                    HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientEducationExaminationCommand serviceCommand)
        {
            var response = await _mediator.Send(serviceCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

      
       [HttpPost]
       public  async Task<object> AddPatientCounsellingInfo([FromBody] AddPatientEducationCommand command)
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
