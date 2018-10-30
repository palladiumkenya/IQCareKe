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
    public class MaternityPatientDeliveryInfoController : Controller
    {
        IMediator _mediator;
        public MaternityPatientDeliveryInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<object> AddDeliveredBabyBirthInfo([FromBody] AddDeliveredBabyBirthInformationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted).ConfigureAwait(false);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpGet("{Id}")]
        public async Task<object> GetDeliveredBabyBirthInfoByPatientDeliveryId(int Id)
        {
            var deliveredBabyInfo = await _mediator.Send(new GetDeliveredBabyBirthInfoQuery { PatientDeliveryInformationId = Id }, HttpContext.RequestAborted);

            if (deliveredBabyInfo.IsValid)
                return Ok(deliveredBabyInfo);

            return BadRequest(deliveredBabyInfo);
        }

        [HttpPost]
        public async Task<object> AddPatientDeliveryInfo([FromBody] AddMaternalPatientDeliveryInfoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetPatientDeliveryInfoByProfileId(int Id)
        {
            var patientDeliveryInfo = await _mediator.Send(new GetPatientDeliveryInformationQuery { ProfileId = Id }, HttpContext.RequestAborted);

            if (patientDeliveryInfo.IsValid)
                return Ok(patientDeliveryInfo);

            return BadRequest(patientDeliveryInfo);
        }

        [HttpPost]
        public async Task<object> DischargePatient([FromBody] DischargePatientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpGet("{Id}")]
        public async Task<object> GetPatientDischargeInfoByMasterVisitId(int Id)
        {
            var response = await _mediator.Send(new GetPatientDischargeInfoQuery { PatientMasterVisitId = Id }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);

        }
    }
}