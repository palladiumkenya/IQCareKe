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
    [Route("api/PostNatalAndBabyExamination")]
    public class PostNatalAndBabyExaminationController : Controller
    {
        IMediator _mediator;
        public PostNatalAndBabyExaminationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> AddPatientExamination([FromBody] AddPostNatalExaminationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }
        [HttpGet]
        public async Task<object> Get(int Id)
        {
            var physicalExamination = await _mediator.Send(new GetPhysicalExaminationQuery { PatientId = Id }, HttpContext.RequestAborted);
            if (physicalExamination.IsValid)
                return Ok(physicalExamination.Value);
            return BadRequest(physicalExamination);
        }

    }
 }