using IQCare.WebApi.Logic.MessageHandler;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using Entity.WebApi.PSmart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.PSmart
{
    [RoutePrefix("api/psmart/{controller}/{Id}")]
    public class FetchShrController : ApiController
    {
        private readonly IIncomingMessageService _incomingMessageService;

        public FetchShrController()
        {
            _incomingMessageService = new IncomingMessageService();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                var clientShr = _incomingMessageService.FetchClientShrNew(id);
                return Ok(clientShr);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] psmartCard psmartCard)
        {
            try
            {
                var clientShr = _incomingMessageService.FetchClientShr(psmartCard);
                return Ok(clientShr);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}