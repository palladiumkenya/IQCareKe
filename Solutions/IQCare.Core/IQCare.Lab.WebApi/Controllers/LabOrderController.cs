using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.BusinessProcess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IQCare.Lab.Core.Models;

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

        [HttpGet("{id}/{status}")]
        public async Task<IActionResult> GetLabOrdersByPatientId(int id, string status="Completed")
        {
            var result = await _mediator.Send(new GetLabOrdersQuery {OrderStatus = status, PatientId = id});

            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabOrderTestsByOrderId(int id)
        {
            var response = await  _mediator.Send(new GetLabTestByOrderId {Id = id});
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpGet("{Id}/{status}")]
        public async Task<IActionResult> GetLabTestResults(int patientId,LabOrderStatus status)
        {
            var response = await _mediator.Send(new GetLabTestResults { PatientId = patientId, Status = status });
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
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