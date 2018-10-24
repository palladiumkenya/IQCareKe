using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Maternity.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MatenityPatientDeliveryInfoController : Controller
    {
        IMediator _mediator;
        public MatenityPatientDeliveryInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<object> AddDeliveredBabyBirthInfo([FromBody] AddDeliveredBabyBirthInformationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpGet("{Id}")]
        public async Task<object> GetDeliveredBabyBirthInfoByPatientDeliveryId(int Id)
        {
            var deliveredBabyInfo = await _mediator.Send(new GetDeliveredBabyBirthInfoQuery
            {
                PatientDeliveryInformationId = Id

            }, Request.HttpContext.RequestAborted).ConfigureAwait(false);

            return Ok(deliveredBabyInfo);
        }
    }
}