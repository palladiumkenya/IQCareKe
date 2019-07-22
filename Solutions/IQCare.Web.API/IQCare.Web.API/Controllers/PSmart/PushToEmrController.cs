using IQCare.WebApi.Logic.MessageHandler;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.PSmart
{


    [RoutePrefix("api/psmart/{controller}/{Id}")]
    public class PushToEmrController : ApiController
    {
        private readonly IIncomingMessageService _incomingMessageService;
        private readonly IIncomingMessageService _psmartMessageService;

        public PushToEmrController()
        {
            _incomingMessageService=new IncomingMessageService();
            _psmartMessageService = new PSmartMesssageService();
        }

        // GET: api/<controller>
        [HttpGet]
        public IHttpActionResult  ClientEligibleList()
        {
            try
            {
               
                var clientList = _incomingMessageService.FetchSmartcardEligibleList();

                if (clientList!=null)
                {
                    
                    return Ok(clientList);
                }
                else
                {
                    return Ok("Empty card list");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
            //TODO : return a list of eligible card list
        }
       [HttpPost]
        public IHttpActionResult ReceiveSHR([FromBody] dynamic shr)
        {

            string message = "SHR Processed Sucessfully!";
            try
            {
                if (shr == null)
                {
                    return BadRequest();
                }
                else
                {
                    var serializer = new JavaScriptSerializer();
                    var jsonObject = serializer.DeserializeObject(shr.ToString());
                    
                    //Task.Run(() =>
                    //{
                     _psmartMessageService.Handle("ForProcessing", shr.ToString());
                  //  });
                }
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// POST api/<controller>
        //[HttpPost]
        //public IHttpActionResult SaveEncryptedShr([FromBody]dynamic shr)
        //{
        //    string message = null;
        //    try
        //    {
        //        if (shr!=null)
        //        {
        //            var result= _incomingMessageService.SaveShrFromMiddleware(shr);
        //            if (result > 0)
        //            {
        //                message="SHR SAVED";
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("Empty SHR");
        //        }
        //        return Ok(message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

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
