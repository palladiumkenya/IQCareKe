using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using Application.Presentation;
using DataAccess.CCC.Repository.Lookup;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.Web
{
    /// <summary>
    /// Summary description for LookupService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LookupService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<LookupCounty> GetLookupWardList(string subcounty)
        {
            LookupCountyRepository lookupCountyRepository = new LookupCountyRepository();
            return lookupCountyRepository.GetWardsList(subcounty);
        }

        [WebMethod]
        public string GetLookupSubcountyList(string county)
        {
            string jsonObject = null;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(county);
            if (lookupCounties != null && lookupCounties.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupCounties);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }
    }
}
