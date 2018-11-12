using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Allergies;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/PatientAllergy")]
    public class PatientAllergyController : Controller
    {
        private readonly IMediator _mediator;

        public PatientAllergyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost("AddAllergy")]
        public async Task<IActionResult> AddAllergy([FromBody] AddAllergiesCommand addAllergyCommand)
        {
            var response = await _mediator.Send(addAllergyCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }
        [HttpGet("GetPatientAllergy")]
        public async Task<IActionResult> GetPatientAllergy(int PatientId)
        {
            var results = await _mediator.Send(new GetPatientAllergies() { PatientId = PatientId }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

    }
}