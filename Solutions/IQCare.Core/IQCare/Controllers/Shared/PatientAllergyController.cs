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
        [HttpGet]
        public async Task<IActionResult> Post([FromBody] AddAllergiesCommand addAllergyCommand)
        {
            var response = await _mediator.Send(addAllergyCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }
        
    }
}