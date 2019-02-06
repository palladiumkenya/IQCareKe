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
        private readonly IMediator _mediator;
        public MaternityPatientDeliveryInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Adds single delivered baby birth info
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

        //Adds collection of delivered baby bith info
        [HttpPost]
        public async Task<object> AddDeliveredBabyBirthInfoCollection([FromBody] AddDeliveredBabyBirthInfoCollectionCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted).ConfigureAwait(false);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        

        [HttpGet("{Id}")]
        public async Task<object> GetDeliveredBabyBirthInfoByPatientDeliveryId(int id)
        {
            var deliveredBabyInfo = await _mediator.Send(new GetDeliveredBabyBirthInfoQuery { PatientDeliveryInformationId = id }, HttpContext.RequestAborted);

            if (deliveredBabyInfo.IsValid)
                return Ok(deliveredBabyInfo.Value);

            return BadRequest(deliveredBabyInfo);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetDeliveredBabyInfoByMasterVisitId(int id)
        {
            var deliveredBabyInfo = await _mediator.Send(new GetDeliveredBabyBirthInfoQuery {  PatientMasterVisitId = id }, HttpContext.RequestAborted);

            if (deliveredBabyInfo.IsValid)
                return Ok(deliveredBabyInfo.Value);

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
        public async Task<object> GetDeliveryInfoByPregnancyId(int id)
        {
            var patientDeliveryInfo = await _mediator.Send(new GetPatientDeliveryInformationQuery { PregnancyId = id }, HttpContext.RequestAborted);

            if (patientDeliveryInfo.IsValid)
                return Ok(patientDeliveryInfo.Value);

            return BadRequest(patientDeliveryInfo);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetDeliveryInfoByMasterVisitId(int id)
        {
            var patientDeliveryInfo = 
                await _mediator.Send(new GetPatientDeliveryInformationQuery {PatientMasterVisitId = id},HttpContext.RequestAborted);

            if (patientDeliveryInfo.IsValid)
                return Ok(patientDeliveryInfo.Value.FirstOrDefault());

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
        public async Task<object> GetDischargeInfoByMasterVisitId(int id)
        {
            var response = await _mediator.Send(new GetPatientDischargeInfoQuery {  PatientMasterVisitId = id }, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);

        }
    }
}