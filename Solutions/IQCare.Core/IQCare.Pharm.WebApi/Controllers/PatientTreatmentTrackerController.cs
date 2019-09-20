using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using IQCare.Pharm.BusinessProcess.CommandHandlers;
using IQCare.Pharm.BusinessProcess.Commands.PatientTreatmentTracker;
using IQCare.Pharm.BusinessProcess.Queries;

namespace IQCare.Pharm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTreatmentTrackerController : ControllerBase
    {

        private readonly IMediator _mediator;
        // GET: api/Lookup

        public PatientTreatmentTrackerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("HasPatientTreatmentStarted/{patientId}")]
        public async Task<IActionResult> HasPatientTreatmentStarted(int patientId)
        {
            var response = await _mediator.Send(new PatientStartTreatmentCommand { PatientId = patientId }, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }
    }
}
