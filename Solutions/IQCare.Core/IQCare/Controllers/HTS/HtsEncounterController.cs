using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess;
using IQCare.HTS.BusinessProcess.Interfaces;
using IQCare.HTS.Core.Interfaces.Repositories;
using IQCare.HTS.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/HtsEncounter")]
    public class HtsEncounterController : Controller
    {
        private readonly IHTSEncounterService _htsEncounterService;

        public HtsEncounterController(IHTSEncounterService htsEncounterService)
        {
            _htsEncounterService = htsEncounterService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EncounterViewModel encounterViewModel)
        {
            _htsEncounterService.addHtsEncounter(encounterViewModel);
            return Ok();
        }
    }
}
