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
        IMediator _mediator;
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

        [HttpGet("{Id}")]
        public async Task<object> GetDiagnosisInfoByPatientId(int Id)
        {
            var diagnosisInfo = await _mediator.Send(new GetPatientDiagnosisInfo { PatientId = Id }, HttpContext.RequestAborted);
            if (diagnosisInfo.IsValid)
                return Ok(diagnosisInfo.Value);
            return BadRequest(diagnosisInfo);
        }

        [HttpPost]
        public async Task<object> AddDrugAdministrationInfo([FromBody] AddMaternalDrugAdministrationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var response = await _mediator.Send(command, HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
       
        }

        [HttpGet("{Id}")]
        public async Task<object> GetDrugAdministrationInfoByPatientId(int Id)
        {
            var response = await _mediator.Send(new GetPatientDrugAdministrationInfoQuery { PatientId = Id },HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);

            return BadRequest(response);
        }



    }
}