using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PatientScreening")]
    public class PatientScreeningController : Controller
    {
        IMediator _mediator;
        public PatientScreeningController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> Add([FromBody] PatientScreeningCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpGet("{PatientId}/{PatientMasterVisitId}")]
        public async Task<object> Get(int PatientId, int PatientMasterVisitId)
        {
            var physicalExamination = await _mediator.Send(new PatientScreeningDetailsQuery { PatientId = PatientId, PatientMasterVisitId = PatientMasterVisitId }, HttpContext.RequestAborted);
            if (physicalExamination.IsValid)
                return Ok(physicalExamination.Value);
            return BadRequest(physicalExamination);
        }

    }
}