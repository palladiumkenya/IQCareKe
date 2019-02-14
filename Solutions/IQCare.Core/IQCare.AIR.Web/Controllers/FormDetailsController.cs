using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.AIR.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/CustomForm")]
    public class FormDetailsController : Controller
    {
        private readonly IMediator _mediator;
        public  FormDetailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("getFormDetails")]
        public async  Task<IActionResult> GetFormDetails (int FormId)
        {
            var response = await _mediator.Send(new GetFormDetailsCommand
            {
                FormId = FormId
            }, Request.HttpContext.RequestAborted);
            if(response.IsValid)
            {
                return Ok(response.Value);
                
            }
            else
            {
                return BadRequest(response);
            }

        }

       
    }
}