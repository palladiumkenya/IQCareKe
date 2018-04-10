using IQCare.WebApi.Logic.PSmart;
using System;
using System.Collections.Generic;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.PSmart
{
    [RoutePrefix("api/psmart/{controller}/{Id}")]
    public class EncrypteShrController : ApiController
    {
        private readonly PsmartStoreManager _psmartStoreManager;

        public EncrypteShrController()
        {
            _psmartStoreManager = new PsmartStoreManager();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "End point", "for saving shr to pstore for transmission" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "End point for saving shr to pstore for transmission";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult ProcessEncryptedShr([FromBody] dynamic encryptedShr)
        {
            try
            {
                int response = _psmartStoreManager.SaveEncryptedPsmartShr(encryptedShr.ToString());
                string responseMessage = null;
                responseMessage = response > 0 ? "Encrypted SHR saved successfully" : "Failed saving Encrypted SHR";
                return Ok(responseMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}