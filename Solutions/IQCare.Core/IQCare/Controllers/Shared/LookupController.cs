using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/Lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupItemViewService _lookupItemViewService;

        public LookupController(ILookupItemViewService lookupItemViewService)
        {
            _lookupItemViewService = lookupItemViewService;
        }

        [HttpGet("byGroupName")]
        public IActionResult Get(string groupName)
        {
            var items = _lookupItemViewService.GetLookupItemsByGroup(groupName);
            return Ok(items);
        }

        [HttpGet("htsOptions")]
        public IActionResult Get()
        {
            string[] options = new string[] {"HTSEntryPoints", "YesNo", "Disabilities", "TestedAs", "Strategy", "TBStatus"};

            var results = _lookupItemViewService.GetHtsOptions(options);
            return Ok(results);
        }
    }
}