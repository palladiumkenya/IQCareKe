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
    public class HivPartnerProfileController : Controller
    {

        private readonly MediatR.IMediator _mediator;
        public HivPartnerProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetHivPartnerProfile(int patientId)
        {
            var response = await _mediator.Send(new GetHivPartnerProfileCommand()
            {
                PatientId = patientId,
                
                
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);

        }


        [HttpPost]
        public async Task<IActionResult> AddHivPartnerProfile([FromBody]AddPatientPartnerProfileCommand addhivpartnerprofilecommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(addhivpartnerprofilecommand, Request.HttpContext.RequestAborted);







            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }



    }
}
