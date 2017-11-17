using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IQCare.Web.API.Controllers
{
    [ApiVersion("1.0")] // deprecate version [ApiVersion( "1.0", Deprecated = true )] [ApiVersion( "2.0" )]
    [Route("api/default/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2_def" };
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
