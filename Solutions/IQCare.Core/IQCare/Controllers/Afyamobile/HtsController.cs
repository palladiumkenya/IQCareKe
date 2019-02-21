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

        [ObsoleteAttribute("This api will soon be deprecated")]
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

        [ObsoleteAttribute("This api will soon be deprecated")]
        [HttpPost("partner")]
        public async Task<IActionResult> PostPartner([FromBody] SynchronizePartnersCommand synchronizePartnersCommand)
        {
            var response = await _mediator.Send(synchronizePartnersCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("partnerdemographics")]
        public async Task<IActionResult> PostPartnerDemographics([FromBody]AfyaMobilePartnersDemographicsCommand partnersDemographicsCommand)
        {
            var response = await _mediator.Send(partnersDemographicsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("familydemographics")]
        public async Task<IActionResult> PostFamilyDemographics([FromBody] AfyaMobileFamilyDemographicsCommand afyaMobileFamilyDemographicsCommand)
        {
            var response = await _mediator.Send(afyaMobileFamilyDemographicsCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("partnerScreening")]
        public async Task<IActionResult> PostPartnerScreening([FromBody]AfyaMobilePartnerScreeningEncounterCommand afyaMobilePartnerScreeningEncounterCommand)
        {
            var response = await _mediator.Send(afyaMobilePartnerScreeningEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("partnerTracing")]
        public async Task<IActionResult> PostPartnerTracing([FromBody] AfyaMobilePartnerTracingEncounterCommand afyaMobilePartnerTracingEncounterCommand)
        {
            var response = await _mediator.Send(afyaMobilePartnerTracingEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [ObsoleteAttribute("This api will soon be deprecated")]
        [HttpPost("family")]
        public async Task<IActionResult> PostFamily([FromBody]SynchronizeFamilyCommand synchronizeFamilyCommand)
        {
            var response = await _mediator.Send(synchronizeFamilyCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("postFamilyScreening")]
        public async Task<IActionResult> PostFamilyScreening([FromBody] AfyaMobileFamilyScreeningEncounterCommand afyaMobileFamilyScreeningEncounterCommand)
        {
            var response = await _mediator.Send(afyaMobileFamilyScreeningEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }


        [HttpPost("postFamilyTracing")]
        public async Task<IActionResult> PostFamilyScreening([FromBody] AfyaMobileFamilyTracingEncounterCommand afyaMobileFamilyTracingEncounterCommand)
        {
            var response = await _mediator.Send(afyaMobileFamilyTracingEncounterCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response);
            return BadRequest(response);
        }
    }
}