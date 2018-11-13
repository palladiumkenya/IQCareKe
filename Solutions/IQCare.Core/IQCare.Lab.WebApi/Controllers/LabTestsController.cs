using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Lab.BusinessProcess.CommandHandlers;
using IQCare.Lab.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Lab.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LabTestsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LabTestsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> GetLabTests([FromBody] string[] labTests)
        {
            var response = await _mediator.Send(new GetLabTestsCommand()
            {
                LabTests = labTests
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{labTestId}")]
        public async Task<IActionResult> GetLabTestPametersByLabTestId(int labTestId)
        {
            var response = await _mediator.Send(new GetLabTestPametersByLabTestIdCommand()
            {
                LabTestId = labTestId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}