using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.AIR.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[Action]")]
    public class ReportingFormController : Controller
    {
        private readonly IMediator _mediator;
        public ReportingFormController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReportingFormDetails(int id)
        {
            var response = await _mediator.Send(new GetReportingFormDetailsQuery { Id = id },
                Request.HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetFormData(int reportingId)
        {
            var response = await _mediator.Send(new GetFormValueQuery()
            {
                Id = reportingId
            }, Request.HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetReportingFormPeriods()
        {
            var response = await _mediator.Send(new GetFormReportingPeriodQuery { }, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReportingFormIndicatorResults(int reportingPeriodId)
        {
            var response = await _mediator.Send(new GetIndicatorResultQuery() {ReportingPeriodId = reportingPeriodId},
                HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response.Value);
        }


    }


}