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
    [Route("api/AddFamilyPlanning")]
    public class AddFamilyPlanningController : Controller
    {
        IMediator _mediator;
        public AddFamilyPlanningController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> AddFamilyPlanning([FromBody] AddPatientFamilyPlanningCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            var diagnosisInfo = await _mediator.Send(new GetPatientFamilyPlanningQuery { PatientId = Id }, HttpContext.RequestAborted);
            if (diagnosisInfo.IsValid)
                return Ok(diagnosisInfo.Value);
            return BadRequest(diagnosisInfo);
        }
    }
}