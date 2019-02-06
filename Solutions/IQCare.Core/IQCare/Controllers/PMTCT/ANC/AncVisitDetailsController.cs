using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands.VisitDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    
    public class AncVisitDetailsController : Controller
    {     
        private readonly IMediator _mediator;

        public AncVisitDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddVisitDetailsCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
        
        [HttpGet("PatientId")]
        public async Task<IActionResult> Get(int patientId)
        {
            if (patientId < 1)
                return BadRequest(patientId);

            var response = await _mediator.Send(new GetVisitDetailsCommand {PatientId = patientId},
                HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        [HttpGet("{patientId}/{serviceAreaId}")]
        public async Task<IActionResult> GetVisitDetailsByVisitType(int patientId, int serviceAreaId )
        {
            if (patientId < 1 || serviceAreaId < 1)
                return BadRequest(patientId + ' ' + serviceAreaId);
            var response = await _mediator.Send(new GetVisitDetailsByServiceAreaIdCommand {PatientId= patientId, ServiceAreaId= serviceAreaId},HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        [HttpGet("{patientId}/{serviceAreaName}")]
        public async Task<IActionResult> GetVisitDetailsByServiceAreaName(int patientId, string serviceAreaName)
        {
            if (patientId < 1)
                return BadRequest(patientId);
            var response = await 
                _mediator.Send(
                    new GetVisitDetailsByServiceAreaNameCommand
                        {PatientId = patientId, ServiceAreaName = serviceAreaName}, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] VisitDetailsCommandEdit command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest(id);
            var response = await _mediator.Send(new DeleteVisitDetailsCommand {Id = id}, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
           return  BadRequest(response.Value);
        }
    }
}