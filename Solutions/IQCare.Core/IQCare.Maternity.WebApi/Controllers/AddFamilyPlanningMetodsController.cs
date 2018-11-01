using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using MediatR;



namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/AddFamilyPlanningMetods")]
    public class AddFamilyPlanningMetodsController : Controller
    {
        IMediator _mediator;
        public AddFamilyPlanningMetodsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> AddFamilyPlanning([FromBody] AddPatientFamilyPlanningMethodCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }
    }
}