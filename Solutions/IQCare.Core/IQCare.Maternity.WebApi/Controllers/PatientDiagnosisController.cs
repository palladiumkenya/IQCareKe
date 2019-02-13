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
    [Route("api/[Controller]/[Action]")]
    public class PatientDiagnosisController : Controller
    {
       private readonly IMediator _mediator;
        public PatientDiagnosisController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<object> AddDiagnosis([FromBody] AddPatientDiagnosisCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result.IsValid)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiagnosis([FromBody] UpdatePatientDiagnosisCommand updatePatientDiagnosisCommand)
        {
            var response = await _mediator.Send(updatePatientDiagnosisCommand, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetDiagnosisInfoByPatientId(int id)
        {
            var diagnosisInfo = await _mediator.Send(new GetPatientDiagnosisInfo { PatientId = id }, HttpContext.RequestAborted);

            if (diagnosisInfo.IsValid)
                return Ok(diagnosisInfo.Value);

            return BadRequest(diagnosisInfo);
        }

        [HttpGet("{Id}")]
        public async Task<object> GetDiagnosisByMasterVisitId(int id)
        {
            var diagnosisInfo = await _mediator.Send(new GetPatientDiagnosisInfo {PatientMasterVisitId = id},
                HttpContext.RequestAborted);

            if (diagnosisInfo.IsValid)
                return Ok(diagnosisInfo.Value.FirstOrDefault());

            return BadRequest(diagnosisInfo);
        }
    }
}