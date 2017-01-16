using IQCare.CCC.UILogic;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
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
        public string GetLookupSubcountyList(string county)
        {
          string jsonObject=  LookupLogic.GeSubCountyListJSON(county);
            //ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(county);
            //if (lookupCounties != null && lookupCounties.Count > 0)
            //{
            //    jsonObject = new JavaScriptSerializer().Serialize(lookupCounties);
            //}
            //else
            //{
            //    jsonObject = "[]";
            //}
            return jsonObject;
        }

        [WebMethod]
        public string GetLookupWardList(string subcounty)
        {
            //string jsonObject;
            //ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //List<LookupCounty> lookupwardsList = lookupManager.GetLookupWards(subcounty);
            //if (lookupwardsList != null && lookupwardsList.Count > 0)
            //{
            //    jsonObject = new JavaScriptSerializer().Serialize(lookupwardsList);
            //}
            //else
            //{
            //    jsonObject = "[]";
            //}
            //return jsonObject;

            string jsonObject = LookupLogic.GetLookupWardListJSON(subcounty);
            return jsonObject;
        }
    }
}
