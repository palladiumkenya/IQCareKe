using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.BusinessProcess.Interfaces;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/Lookup")]
    public class LookupController : Controller
    {
        private readonly IMediator _mediator;

        public LookupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("byGroupName")]
        public async Task<IActionResult> Get(string groupName)
        {
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand {GroupName = groupName},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("GetbyGroupName/{groupName}")]
        public async Task<IActionResult> GetbyGroupName(string groupName)
        {
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = groupName },
                HttpContext.RequestAborted);
            
            if (results.IsValid)
                return Ok( results.Value);

            return BadRequest(results);
        }


        [HttpGet("htsOptions")]
        public async Task<IActionResult> Get()
        {
            string[] options = new string[] { "HTSEntryPoints", "YesNo", "Disabilities", "TestedAs", "Strategy", "TbScreening", "ReasonsPartner", "HIVResults", "HIVTestKits", "HIVFinalResults" };

            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);


            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("optionsByGroupandItemName")]
        public async Task<IActionResult> Get(string groupName, string itemName)
        {
            var results =
                await _mediator.Send(
                    new GetOptionsByGroupAndItemNameCommand {GroupName = groupName, ItemName = itemName},
                    HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpGet("getIdentifyerTypes")]
        public async Task<IActionResult> GetIdentifierType()
        {
            var results = await _mediator.Send(new GetPersonIdentificationCommand { CodeName = "PersonIdentification" }, HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }

        [HttpGet("getocc")]
        public async Task<IActionResult>  GetOccupations()
        {
            string options =  "Occupation";
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = options }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

       
       }
        [HttpGet("getEducation")]
        public async Task<IActionResult> GetRegConsentEducationOptions()
        {
            string options = "EducationalLevel";
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName =options }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }

        [HttpGet("getRegConsentOptions")]
        public async Task<IActionResult> GetRegConsentOptions()
        {
            string options = "ConsentOptions";
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = options }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }
         
        [HttpGet("getMaritalStatusOptions")]
        public async Task<IActionResult> GetMaritalStatusOptions()
        {
            string options =   "MaritalStatus" ;
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = options }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("getContactType")]
        public async Task<IActionResult> GetContactType()
        {
            string[] options = new string[] {"PersonContactType"};
            var results = await _mediator.Send(new GetRegistrationOptionsCommand { RegistrationOptions = options }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }
        [HttpGet("getRelOptions")]
        public async Task<IActionResult> GetRelationshipOptions()
        {
            string options =  "Relationship" ;
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = options },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("getGenderOptions")]
        public async Task<IActionResult> GetGenderOptions()
        {
            string options =  "Gender";
            var results = await _mediator.Send(new GetOptionsByGroupNameCommand { GroupName = options },
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("registrationOptions")]
        public async Task<IActionResult> GetOptions()
        {
            string[] options = new string[] { "HTSMaritalStatus", "HTSKeyPopulation", "Gender", "Relationship", "PriorityPopulation" };
            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("htsTracingOptions")]
        public async Task<IActionResult> GetTracingOptions()
        {
            string[] options = new string[] { "TracingMode", "TracingOutcome", "TracingType" };
            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);

            if (results.IsValid)
                return Ok(results.Value);

            return BadRequest(results);
        }

        [HttpPost("getCustomOptions")]
        public async Task<IActionResult> GetCustomOptions([FromBody] string[] options)
        {
            var results = await _mediator.Send(new GetRegistrationOptionsCommand {RegistrationOptions = options},
                HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpPost("getCounty")]
        public async Task<IActionResult> GetCounty([FromBody] GetCountyCommand getCountyCommand)
        {
            var results = await _mediator.Send(getCountyCommand, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("getFacilityList")]
        public async Task<IActionResult> GetFacilityList()
        {
            var results = await _mediator.Send(new GetFacilityListCommand(), HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("searchFacilityList")]
        public async Task<IActionResult> SearchFacilityList(string searchString)
        {
            var results = await _mediator.Send(new GetFilteredFacilityListCommand(){ searchString = searchString }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }


        [HttpGet("getConsentType")]
        public async Task<IActionResult> GetConsentOptions()
        {
            var results = await _mediator.Send(new GetConsentTypeCommand() { ItemName = "ConsentToSendSMS" }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);
        }

        [HttpGet("getCountylist")]
        public async Task<IActionResult> GetCountyList(string countyid,string subcountyid)
        {
            var results = await _mediator.Send(new GetCountiesCommand() { CountyId = countyid, SubcountyId = subcountyid }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }


        [HttpGet("getSubCountylist")]
        public async Task<IActionResult> GetSubCountyList(string countyid, string subcountyid)
        {
            var results = await _mediator.Send(new GetSubCountiesCommand() { CountyId = countyid, SubcountyId = subcountyid }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }


        [HttpGet("getWardlist")]
        public async Task<IActionResult> GetWardList(string countyid, string subcountyid)
        {
            var results = await _mediator.Send(new GetWardCommand() { CountyId = countyid, SubcountyId = subcountyid }, HttpContext.RequestAborted);
            if (results.IsValid)
                return Ok(results.Value);
            return BadRequest(results);

        }




    }

        //[HttpGet("getFacility/{mflCode}")]
        //public async Task<IActionResult> GetFacility(string mflCode)
        //{
        //    var response = await _mediator.Send(new GetFacilityCommand() {MflCode = mflCode},
        //        Request.HttpContext.RequestAborted);
        //    if (response.IsValid)
        //        return Ok(response.Value);
        //    return BadRequest(response);
        //}
    //} 
}