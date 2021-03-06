﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using IQCare.AIR.BusinessProcess.Command;

namespace IQCare.AIR.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ReportingFormController : Controller
    {
        private readonly IMediator _mediator;
        public ReportingFormController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportingFormDetails(int id)
        {
            var response = await _mediator.Send(new GetReportingFormDetailsQuery { Id = id }
           );

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormData(int id)
        {
            var response = await _mediator.Send(new GetFormValueQuery()
            {
                Id = id
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
        public async Task<IActionResult> GetReportingFormIndicatorResults(int id)
        {
            var response = await _mediator.Send(new GetIndicatorResultQuery() {ReportingPeriodId = id},
                HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response.Value);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReportingSections(int id)
        {
            var response = await _mediator.Send(new GetFormSectionsQuery() { Id = id },
                HttpContext.RequestAborted);
            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response.Value);
        }
        [HttpPost]
        public async Task<IActionResult> PeriodExists([FromBody] PeriodExistCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetConfiguredReportingForms()
        {
            var response = await _mediator.Send(new GetConfiguredReportingFormsQuery(), HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


    }


}