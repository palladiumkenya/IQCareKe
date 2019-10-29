using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.BusinessProcess.Commands.Matrix;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.HTS.BusinessProcess.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/Facility")]
    public class FacilityController : Controller
    {
        private readonly IMediator _mediatR;

        public FacilityController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _mediatR.Send(new GetFacilityListCommand { }, HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("GetActiveFacility")]
        public async Task<object> GetActiveFacility()
        {
            var response = await _mediatR.Send(new GetActiveFaciltyCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetAppointmentStatistics/{summaryDate}")]
        public async Task<IActionResult> GetAppointmentStatistics(DateTime summaryDate)
        {
            var response = await _mediatR.Send(new GetAppointmentStatisticsCommand() {
                summaryDate = summaryDate
            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetAllCCCVisitCount/{summaryDate}")]
        public async Task<IActionResult> GetAllCCCVisitCount(DateTime summaryDate)
        {
            var response = await _mediatR.Send(new GetAllCCCVisitCountCommand() { SummaryDate = summaryDate }, Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetILStatistics")]
        public async Task<IActionResult> GetILStatistics()
        {
            var response = await _mediatR.Send(new GetILStatisticsCommand()
            {

            }, Request.HttpContext.RequestAborted);

            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetFacilityCareEndingSummary")]
        public async Task<IActionResult> GetFacilityCareEndingSummary()
        {
            var response = await _mediatR.Send(new GetFacilityCareEndingCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetFamilyTestingStatistics")]
        public async Task<IActionResult> GetFamilyTestingStatistics()
        {
            var response = await _mediatR.Send(new TestingSummaryStatisticsQuery(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetPatientStabilitySummary")]
        public async Task<IActionResult> GetPatientStabilitySummary()
        {
            var response = await _mediatR.Send(new GetPatientStabilitySummaryCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetILMessageStats")]
        public async Task<IActionResult> GetILMessageStats()
        {
            var response = await _mediatR.Send(new GetILMessageStatsCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetHtsFacilityStatistics")]
        public async Task<IActionResult> GetHtsFacilityStatistics()
        {
            var response = await _mediatR.Send(new GetHtsFacilityStatisticsCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }

        [HttpGet("GetDuplicatePersons")]
        public async Task<IActionResult> GetDuplicatePersons()
        {
            var response = await _mediatR.Send(new GetDuplicatePersonsCommand(), Request.HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response);
        }
    }
}
