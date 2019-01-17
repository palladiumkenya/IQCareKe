using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Lab.BusinessProcess.CommandHandlers;
using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.BusinessProcess.Queries;
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
        public async Task<IActionResult> GetFilteredLabTests([FromBody] string[] labTests)
        {
            var response = await _mediator.Send(new GetLabTestsCommand
            {
                LabTests = labTests
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value.Select(l => new KeyValuePair<string, LabTestViewModel>(l.Name, l)).ToList());
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLabTests()
        {
            var response = await _mediator.Send(new GetLabTestsCommand(), Request.HttpContext.RequestAborted);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestResultsByLabOrderTestId(int id)
        {
            var result = await _mediator.Send(new GetLabOrderTestResults {LabOrderTestId = id},
                Request.HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);
            return BadRequest(result);
        }


    }
}