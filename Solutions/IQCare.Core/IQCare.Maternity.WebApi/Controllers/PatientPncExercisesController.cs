using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PatientPncExercisesController : Controller
    {
        private readonly IMediator _mediator;

        public PatientPncExercisesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> Get(int patientId, int patientMasterVisitId)
        {
            var response = await _mediator.Send(new GetPatientPncExercisesCommand()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId
            }, Request.HttpContext.RequestAborted);

            if(response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddPatientPncExercisesCommand addPatientPncExercisesCommand)
        {
            var response = await _mediator.Send(addPatientPncExercisesCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}