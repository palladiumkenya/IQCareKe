using IQCare.WebApi.Logic.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IQCare.Web.Api.Controllers.Config
{
    [RoutePrefix("api/config/{controller}/{Id}")]
    public class ConnectionController : ApiController
    {
        readonly string TOKEN = "31LgBzXbDSPj28AwWars8Q==";
        readonly IQConfig ConfigHelper;
        public ConnectionController()
        {
            ConfigHelper = new IQConfig();
            
        }
       
        [HttpGet]
        public string Get()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("TOKEN");
            var token = headerValues.FirstOrDefault();
            if(token !=null && token == TOKEN)
            {
                return ConfigHelper.GetIQCareConnectionString();
            }
            return "INVALID-ATTEMP";
        }

       
    }
}
