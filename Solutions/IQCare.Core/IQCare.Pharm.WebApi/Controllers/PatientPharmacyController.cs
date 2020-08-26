using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy;
using MediatR;

namespace IQCare.Pharm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientPharmacyController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PatientPharmacyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getPharmacyDrugList")]
        public async Task<IActionResult> GetPharmacyDrugList([FromQuery] DrugListSearchQuery sq)
        {
            var response = await _mediator.Send(new
                GetPharmacyDrugListCommand
            { pmscm = sq.pmscm, tp = sq.tp, filteritem = sq.filteritem }, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }


        [HttpGet("getPharmacyCurrentRegimen/{patientId}")]
        public async Task<IActionResult> GetPharmacyCurrentRegimen(int patientId)
        {
            var response = await _mediator.Send(new GetPharmacyCurrentRegimenCommand { PatientId = patientId }, HttpContext.RequestAborted);

            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet("getPharmacyVisitDrugDetails/{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetPharmacyVisitDrugDetails(int patientId,int patientMasterVisitId)
        {
            var response = await _mediator.Send(new GetPatientPharmacyDetailsCommand { PatientId = patientId,PatientMasterVisitId=patientMasterVisitId }, HttpContext.RequestAborted);
            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet("getPharmacyVisit/{patientId}")]
        public async Task<IActionResult> GetPharmacyVisit(int patientId)
        {
            var response =await _mediator.Send(new GetPatientPharmacyVisitCommand{ PatientId =patientId},HttpContext.RequestAborted);
            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response.Value);
        }

        [HttpGet("getPharmacyRegimens/{regimenline}")]
        public async Task<IActionResult> GetPharmacyRegimens(string regimenline)
        {
            var response = await _mediator.Send(new GetPharmacyRegimensCommand
            {
                LookupName = regimenline
            }, HttpContext.RequestAborted);

            if (!response.IsValid)
            {
                return BadRequest(response);
            }
            return Ok(response.Value);
        }

        [HttpGet("getDrugBatches/{drugpk}")]
            public async Task<IActionResult> GetDrugBatches(string drugpk)
        {
            var response = await _mediator.Send(new GetPharmacyDrugBatchCommand
            {
                 Drug_Pk =drugpk
            }, HttpContext.RequestAborted);

            if (!response.IsValid)
            {
                return BadRequest(response);
            }
            return Ok(response.Value);

        }

        [HttpPost("saveUpdatePharmacy")]
        public async Task<IActionResult> SaveUpdatePharmacy([FromBody]SaveUpdatePatientPharmacyCommand saveUpdatePatientPharmacyCommand)
        {
            var response = await _mediator.Send(saveUpdatePatientPharmacyCommand, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}