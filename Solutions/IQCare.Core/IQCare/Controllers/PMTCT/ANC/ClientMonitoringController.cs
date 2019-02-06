using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.PMTCT.ANC
{
    [Produces("application/json")]
    [Route("api/ClientMonitoring")]
    public class ClientMonitoringController : Controller
    {
        private readonly IMediator _mediator;

        public ClientMonitoringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientMonitoringCommand clientMonitoringCommand)
        {

            var response = await _mediator.Send(new ClientMonitoringCommand
            {
                PatientId = clientMonitoringCommand.PatientId,
                PatientMasterVisitId=clientMonitoringCommand.PatientMasterVisitId,
                WhoStage=clientMonitoringCommand.WhoStage,
                ServiceAreaId = clientMonitoringCommand.ServiceAreaId,
                ClinicalNotes = clientMonitoringCommand.ClinicalNotes,
                ScreeningTypeId = clientMonitoringCommand.ScreeningTypeId,
                ScreeningDone = clientMonitoringCommand.ScreeningDone,
                ScreeningDate = clientMonitoringCommand.ScreeningDate,
                cacxMethod = clientMonitoringCommand.cacxMethod,
                cacxResult = clientMonitoringCommand.cacxResult,
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClientMonitoringCommand command)
        {
            var response = await _mediator.Send(new ClientMonitoringCommand
            {
                PatientId = command.PatientId,
                PatientMasterVisitId = command.PatientMasterVisitId,
                WhoStage = command.WhoStage,
                ServiceAreaId = command.ServiceAreaId,
                ClinicalNotes = command.ClinicalNotes,
                ScreeningTypeId = command.ScreeningTypeId,
                ScreeningDone = command.ScreeningDone,
                ScreeningDate = command.ScreeningDate,
                cacxMethod = command.cacxMethod,
                cacxResult = command.cacxResult,
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
            {
                return Ok(response.Value);
            }
            return BadRequest(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
