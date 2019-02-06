using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.PMTCT.HEI
{
    [Produces("application/json")]
    [Route("api/DeliveryMaternalHistory")]
    public class DeliveryMaternalHistoryController : Controller
    {
        private readonly IMediator _mediator;

        public DeliveryMaternalHistoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HEIDeliveryCommand heiDeliveryCommand)
        {
            var response = await _mediator.Send(heiDeliveryCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("UpdateHeiEncounter")]
        public async Task<IActionResult> UpdateHeiEncounter([FromBody]UpdateHeiDeliveryCommand updateHeiDeliveryCommand)
        {
            var response = await _mediator.Send(updateHeiDeliveryCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpPost("UpdateOutComeAt24Months")]
        public async Task<IActionResult> UpdateOutComeAt24Months([FromBody] UpdateHeiDeliveryAt24MonthsCommand updateHeiDeliveryAt24MonthsCommand)
        {
            var response = await _mediator.Send(updateHeiDeliveryAt24MonthsCommand, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> Get(int patientId, int patientMasterVisitId)
        {
            var response = await _mediator.Send(new GetHeiDeliveryCommand()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}