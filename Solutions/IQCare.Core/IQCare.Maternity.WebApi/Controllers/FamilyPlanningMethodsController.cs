using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using MediatR;
using IQCare.Maternity.BusinessProcess.Queries.PNC;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class FamilyPlanningMethodsController : Controller
    {
        IMediator _mediator;
        public FamilyPlanningMethodsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> AddFamilyPlanning([FromBody] AddPatientFamilyPlanningMethodCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFamilyPlanningMethod([FromBody]UpdatePatientFamilyPlanningMethodCommand updatePatientFamilyPlanningMethodCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(updatePatientFamilyPlanningMethodCommand);
            var response = await _mediator.Send(updatePatientFamilyPlanningMethodCommand,
                Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetFamilyPlanningInfo(int Id)
        {
            var getPatientFamilyPlanningMethods = await _mediator.Send(new GetPatientFamilyPlanningMethodQuery { PatientId = Id }, HttpContext.RequestAborted);
            if (getPatientFamilyPlanningMethods.IsValid)
                return Ok(getPatientFamilyPlanningMethods.Value);
            return BadRequest(getPatientFamilyPlanningMethods);
        }
    }
}