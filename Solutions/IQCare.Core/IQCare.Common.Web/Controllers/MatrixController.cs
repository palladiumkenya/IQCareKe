using IQCare.Common.BusinessProcess.Commands.Matrix;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Common.Web.Controllers
{
    [Route("api/[controller]")]
    public class MatrixController : Controller
    {
        private readonly IMediator _mediator;

        public MatrixController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
      /*  [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        [HttpGet]
        public async Task<IActionResult> GetMatrix()
        {
            var response = await _mediator.Send(new MatrixCommand(), HttpContext.RequestAborted);
            if (response.IsValid)
                return Ok(response.Value);
            return BadRequest(response.Value);
        }


        // GET api/<controller>/5
       /* [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
