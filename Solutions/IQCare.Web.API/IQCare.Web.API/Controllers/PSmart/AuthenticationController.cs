using IQCare.WebApi.Logic.PSmart;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.PSmart
{
    [RoutePrefix("api/psmart/{controller}/{Id}")]
    public class AuthenticationController : ApiController
    {
        private readonly IPSmartAuthRequest _authRequest;

        public AuthenticationController()
        {
            _authRequest = new PSmartAuthManager();
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
        public IHttpActionResult Authenticate([FromBody] dynamic userAuth)
        {
            /*
                       if (string.IsNullOrEmpty(userAuth.UserName) || string.IsNullOrEmpty(userAuth.Password))
                       {
                           return BadRequest();
                       }
                       string response = _authRequest.Authentication(userAuth.UserName, userAuth.Password);
                      // var serializer = new JavaScriptSerializer();
                       //var jsonObject = serializer.DeserializeObject(authUser.ToString());

                       //TODO: Pass the data to the Authentication function
                       return Ok(response);
                       */
            if (userAuth == null)
            {
                return BadRequest();
            }

            //call incoming handlers
            var serializer = new JavaScriptSerializer();
            var jsonObject = serializer.DeserializeObject(userAuth.ToString());
            string username = "", password = "";

            foreach (var item in jsonObject)
            {
                if (item.Key == "USERNAME")
                {
                    username = item.Value;
                }
                if (item.Key == "PASSWORD")
                {
                    password = item.Value;
                }
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }

            var response = _authRequest.Authentication(username, password);
            return Ok(response);
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