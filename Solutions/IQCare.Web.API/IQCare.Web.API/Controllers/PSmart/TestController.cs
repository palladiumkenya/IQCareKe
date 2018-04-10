using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using IQCare.Events;
using IQCare.WebApi.Logic.MessageHandler;

namespace IQCare.Web.Api.Controllers.PSmart
{

    [RoutePrefix("api/interop/{controller}/{Id}")]
    public class TestController : ApiController
    {



        private readonly IOutgoingMessageService _outgoingMessageService;
        public TestController()
        {
            _outgoingMessageService = new OutgoingMessageService();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MessageEventArgs message)
        {
            _outgoingMessageService.Handle(message);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }

    }
}
