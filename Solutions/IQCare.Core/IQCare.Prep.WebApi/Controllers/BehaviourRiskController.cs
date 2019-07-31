using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Prep.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Prep.WebApi.Controllers
{


    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class BehaviourRiskController : Controller
    {
       


        private readonly MediatR.IMediator _mediator;
        public BehaviourRiskController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{personId}")]
        public async Task<IActionResult> GetRiskAssessmentVisitsDetails(int personId)
        {
            var response = await _mediator.Send(new RiskAssessmentVisitQuery()
            {
                PersonId= personId
              
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddAssessmentVisitDetail([FromBody] RiskAssessmentVisitDetailCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }


        [HttpPost]
        public async Task<IActionResult> GetAssessmentFormDetails([FromBody] GetRiskAssessmentDetailsCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Encounterexists([FromBody] CheckRiskAssessmentEncounterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }
    }
}