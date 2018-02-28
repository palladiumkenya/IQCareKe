using IQCare.HTS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Controllers.HTS
{
    [Produces("application/json")]
    [Route("api/Modules")]
    public class ModulesController : Controller
    {
        private readonly IModuleRepository _moduleRepository;

        public ModulesController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var modules = _moduleRepository.GetAll();
            return Ok(modules);
        }
    }
}
