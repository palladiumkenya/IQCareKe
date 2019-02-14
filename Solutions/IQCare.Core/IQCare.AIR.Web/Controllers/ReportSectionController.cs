using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.AIR.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[Action]")]
    public class ReportSectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportSectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReportSectionByFormId(int id)
        {
            var response = await _mediator.Send(new GetReportSectionQuery {FormId = id}, HttpContext.RequestAborted);

            if(!response.IsValid)
              return BadRequest(response);

            return Ok(response.Value);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReportSubSections(int sectionId)
        {
            var response = await _mediator.Send(new GetReportSubSectionsQuery() {SectionId = sectionId},
                HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }


    }
}