using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Records;
using Entities.Records.Enrollment;
using IQCare.Records.UILogic;
using Microsoft.AspNetCore.Mvc;
using IQCare.Records.UILogic.Enrollment;


namespace Records.Controllers
{

    public class RegistrationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/Records/GetGenderList")]
        public IActionResult GetGenderDetails()
        {
          
                var Lookup = new LookupLogic();
                IList<LookupItemView> gender = Lookup.GetListGenderOptions();
                return new OkObjectResult(gender);
            
           
        }

        [HttpGet]
        [Route("api/Records/GetMaritalStatus")]
        public IActionResult GetMaritalStatusDetails()
        {
           
                var Lookup = new LookupLogic();
                IList<LookupItemView> maritalstatus = Lookup.GetLookItemByGroup("MaritalStatus");
                return new OkObjectResult(maritalstatus);
            
           
        }
        [HttpGet]
        [Route("api/Records/GetOccupationList")]
        public IEnumerable<LookupItemView> GetOccupationList()
        {
            try
            {
                var Lookup = new LookupLogic();
                IList<LookupItemView> occupationlist = Lookup.GetLookItemByGroup("Occupation");
                return occupationlist;
            }
            catch (AppException ex)
            {
                throw ex;

            }
        }
        [HttpGet]
        [Route("api/Records/GetIdentifyerType")]
        public IActionResult GetMultipleIdentifierList()
        {

            
                var Idm = new IdentifierManager();
                IList<Identifier> identifyertype = Idm.GetMultipleIdentifierByCode("PersonIdentification");
            return new OkObjectResult(identifyertype);

          
        }

        [HttpGet]
        [Route("api/Records/GetCounty")]
        [ProducesResponseType(200, Type = typeof(LookupCounty))]
        public IActionResult GetCounty()
        {
            LookupLogic lct = new LookupLogic();
           IList<LookupCounty> counties= lct.GetLookupCounties();
           return new OkObjectResult(counties);
           
        }


       [HttpGet]
       [Route("api/Records/GetSubCounty/{county?}")]
       public IActionResult GetSubCounty(string county)
        {
            LookupLogic lct = new LookupLogic();
        IList<LookupCounty> subcounties = lct.GetSubCountyList(county);
           return new OkObjectResult(subcounties);
            
        }

        [HttpGet]
        [Route("api/Records/GetLookupWard/{subcounty?}")]
        public IActionResult GetLookupWardList(string subcounty)
        {
            LookupLogic lct = new LookupLogic();
            IList<LookupCounty> wards = lct.GetLookupWardList(subcounty);
            return  new OkObjectResult(wards);
            
        }
        [HttpGet]
        [Route("api/Records/GetEducationalList")]
        public IActionResult GetEducationalist()
        {
           
                var Lookup = new LookupLogic();
                IList<LookupItemView> educationlist = Lookup.GetLookItemByGroup("EducationLevel");
            return new OkObjectResult(educationlist);
             
           
        }


     
       
    }
}