using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.PNCCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using IQCare.Maternity.Core;


namespace IQCareMaternityWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PatientVisitDetails")]
    public class PatientVisitDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientVisitDetailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientVisitDetails([FromBody]PatientVisitDetailsCommand addPatientVisitDetailsCommand)
        {
            var response = await _mediator.Send(addPatientVisitDetailsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}