using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;

namespace IQCare.Controllers.Afyamobile
{
    [Produces("application/json")]
    [Route("api/Hts")]
    public class HtsController : Controller
    {
        private readonly IMediator _mediator;

        public HtsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("indexclient")]
        public async Task<IActionResult> Post([FromBody] SynchronizeClientsCommand synchronizeClientsCommand)
        {
            var response = await _mediator.Send(synchronizeClientsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("demographics")]
        public async Task<IActionResult> PostDemographics([FromBody]AfyaMobileSynchronizeClientsCommand afyaMobileSynchronizeClientsCommand)
        {
            var response = await _mediator.Send(afyaMobileSynchronizeClientsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("htsPretest")]
        public async Task<IActionResult> PostEncounter([FromBody]AfyaMobileSynchronizeEncounterCommand afyaMobileSynchronizeEncounterCommand)
        {
            var response = await _mediator.Send(afyaMobileSynchronizeEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("htsTests")]
        public async Task<IActionResult> PostTests([FromBody]AfyaMobileSynchronizeHtsTestsCommand afyaMobileSynchronizeHtsTestsCommand)
        {
            var response = await _mediator.Send(afyaMobileSynchronizeHtsTestsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("htsReferral")]
        public async Task<IActionResult> PostReferral([FromBody]AfyaMobileSynchronizeReferralCommand afyaMobileSynchronizeReferralCommand)
        {
            var response = await _mediator.Send(afyaMobileSynchronizeReferralCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("htsLinkage")]
        public async Task<IActionResult> PostLinkage([FromBody]AfyaMobileSynchronizeLinkageCommand afyaMobileSynchronizeLinkageCommand)
        {
            var response =
                await _mediator.Send(afyaMobileSynchronizeLinkageCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("htsTracing")]
        public async Task<IActionResult> PostTracing([FromBody]AfyaMobileSynchronizeTracingCommand afyaMobileSynchronizeTracingCommand)
        {
            var response =
                await _mediator.Send(afyaMobileSynchronizeTracingCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("partner")]
        public async Task<IActionResult> PostPartner([FromBody] SynchronizePartnersCommand synchronizePartnersCommand)
        {
            var response = await _mediator.Send(synchronizePartnersCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("family")]
        public async Task<IActionResult> PostFamily([FromBody]SynchronizeFamilyCommand synchronizeFamilyCommand)
        {
            var response = await _mediator.Send(synchronizeFamilyCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }
    }
}