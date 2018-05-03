using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using IQCare.WebApi.Logic.MessageHandler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.Interop
{
    [RoutePrefix("api/interop/{controller}")]
    public class ReceiveController : ApiController
    {
        private readonly IIncomingMessageService _incomingMessageService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ReceiveController()
        {
            _incomingMessageService = new IncomingMessageService();
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/receive
        [HttpPost]
        public IHttpActionResult Post([FromBody] dynamic request)
        {
            if (request == null)
            {
                return BadRequest();
            }

           // log.Debug($"Recieved {request}");

            //call incoming handlers
            var serializer = new JavaScriptSerializer();
            var jsonObject = serializer.DeserializeObject(request.ToString());
            string messageType = null;

            foreach (var item in jsonObject)
            {
                if (item.Key == "MESSAGE_HEADER")
                {
                    foreach (var val in item.Value)
                    {
                        if (val.Key == "MESSAGE_TYPE")
                        {
                            messageType = val.Value;
                            break;
                        }
                    }
                }
                break;
            }

            Task.Run(() =>
            {
               // log.Debug("beginning async...");
                _incomingMessageService.Handle(messageType, request.ToString());
                //log.Debug("end...");
                return String.Empty;
            });

           // log.Debug($"End Received {request}");
           // log.Debug("Sent OK response");
            return Ok(new { success = true });
        }

        // PUT api/values/5
        [HttpGet]
        public void Put(int id, [System.Web.Http.FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpGet]
        public void Delete(int id)
        {

        }
    }
}