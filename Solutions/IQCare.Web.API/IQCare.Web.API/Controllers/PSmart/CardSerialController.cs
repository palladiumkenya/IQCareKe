using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using Entity.WebApi.PSmart;
using IQCare.WebApi.Logic.MessageHandler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.PSmart
{
    [RoutePrefix("api/psmart/{controller}/{Id}")]
    public class CardSerialController : ApiController
    {
        private readonly IIncomingMessageService _incomingMessageService;
        

        public CardSerialController()
        {
            _incomingMessageService=new IncomingMessageService();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult LoadFromEmr(string CARD_SERIAL_NO)
        {
            if (string.IsNullOrEmpty(CARD_SERIAL_NO))
            {
                return BadRequest("Missing Card Serial Number");
            }
            else
            {
                var result = _incomingMessageService.LoadFromEmr(CARD_SERIAL_NO);
                return Ok(result);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult ProcessCardSerialNumber([FromBody] dynamic _psmartCard)
        {
            var serializer = new JavaScriptSerializer();
            var jsonObject = serializer.DeserializeObject(_psmartCard.ToString());
            string CARD_SERIAL_NO = ""; int PATIENTID=0;

            foreach (var item in jsonObject)
            {
                if (item.Key == "CARD_SERIAL_NO")
                {
                    CARD_SERIAL_NO = item.Value;
                }
                if (item.Key == "PATIENTID")
                {
                    PATIENTID =Convert.ToInt32(item.Value);
                }
            }
            psmartCard _card=new psmartCard()
            {
                CARD_SERIAL_NO = CARD_SERIAL_NO,
                PATIENTID = PATIENTID
            };
            //psmartCard.PATIENTID = 30730;
            // psmartCard.CARD_SERIAL_NO = "12345-abcedw-098765";

            if (string.IsNullOrEmpty(_card.CARD_SERIAL_NO))
            {
                return BadRequest();
            }
            else
            {
                //TODO saving to Greencard Tables
                string processCardStatus= _incomingMessageService.ProcessCardSerialNumberIdentifier(_card);
                var result = _incomingMessageService.ProcessCardSerialNumberIdentifierBluecard(_card);

                return Ok(result);
                //TODO return SHR after saving the serial.
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
