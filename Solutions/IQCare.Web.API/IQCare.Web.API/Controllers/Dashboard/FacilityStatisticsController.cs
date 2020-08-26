using IQCare.WebApi.Logic.MessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IQCare.Web.Api.Controllers.Dashboard
{
    [RoutePrefix("api/dashboard/{controller}")]
    public class FacilityStatisticsController : ApiController
    {
        private readonly IFacilityDashboardService _facilityDashboardService;

        public FacilityStatisticsController()
        {
            _facilityDashboardService = new FacilityDashboardService();
        }

        [HttpGet]
        public IHttpActionResult Get(DateTime appointmentDate)
        {
            return Ok(_facilityDashboardService.GetAppointmentSummaryByDate(appointmentDate));
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
