using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IQCare.Web.ApiLogic.MessageHandler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.API.Controllers.Interop
{
    [ApiVersion("1.0")] // deprecate version [ApiVersion( "1.0", Deprecated = true )] [ApiVersion( "2.0" )]
    [Route("api/interop/v{version:apiVersion}/[controller]")]
    public class IQILController : Controller
    {
        readonly IIncomingMessageService _incomingMessageService;
        public IQILController(IIncomingMessageService incomingMessageService)
        {
            _incomingMessageService = incomingMessageService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            IncomingMessageService sc = 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
