using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQCare.Lab.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LabOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LabOrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        public IActionResult ApiStatus()
        {
            return Ok("Lab order api is running");
        }

        [HttpPost]
        public async Task<IActionResult> AddLabOrder([FromBody]AddLabOrderCommand addLabOrderCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addLabOrderCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetLabOrderTestResultsByPatientId(int id)
        {
            var labTestResults = await _mediator.Send(new GetLabTestResults { PatientId = id });
            return Ok(labTestResults);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLabOrder([FromBody] CompleteLabOrderCommand completeLabOrder)
        {
            var labOrderResponse = await _mediator.Send(completeLabOrder).ConfigureAwait(false);
            if (labOrderResponse.IsValid)
                return Ok(labOrderResponse.Value);
            return BadRequest(labOrderResponse);
        }
    }
}