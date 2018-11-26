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
    public class PatientDrugAdministrationController : Controller
    {
        private readonly IMediator _mediator;
        public PatientDrugAdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<object> Add([FromBody] AddPatientDrugAdministrationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);

        }

        [HttpGet("{Id}")]
        public async Task<object> GetByPatientId(int id)
        {
            var response = await _mediator.Send(new GetPatientDrugAdministrationInfoQuery { PatientId = id }, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpGet("{patientId}/{patientMasterVisitId}")]
        public async Task<IActionResult> GetByPatientIdAndPatientMasterVisitId(int patientId, int patientMasterVisitId)
        {
            var response = await _mediator.Send(new GetPatientDrugAdministrationByVisitInfoQuery()
            {
                PatientMasterVisitId = patientMasterVisitId,
                PatientId = patientId
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetById(int id)
        {
            var response = await _mediator.Send(new GetPatientDrugAdministrationById {Id = id},
                HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<object> Edit([FromBody]UpdateDrugAdministrationCommand command)
        {
            var response = await  _mediator.Send(command, HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

        [HttpDelete("{Id}")]
        public async Task<object> Deactivate(int id)
        {
            var response = await _mediator.Send(new DeactivateDrugAdministrationCommand {Id = id},
                HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }

    }
}