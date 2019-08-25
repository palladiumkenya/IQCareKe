using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IQCare.Prep.BusinessProcess.Commands;
using MediatR;

namespace IQCare.Prep.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]

    public class MonthlyRefillController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MonthlyRefillController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<IActionResult> AddMonthlyRefill([FromBody]AddMonthlyRefillCommand addmonthyrefillcommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addmonthyrefillcommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}/{serviceAreaId}")]
        public async Task<IActionResult> GetMonthlyRefillDetails (int patientId, int patientMasterVisitId,int serviceAreaId)
        {
            var response = await _mediator.Send(new GetMonthlyRefillDetailsCommand()
            {
                PatientId = patientId,
                PatientMasterVisitId=patientMasterVisitId,
                ServiceAreaId=serviceAreaId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


    }
}