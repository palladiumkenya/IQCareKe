
using System.Web.Services;
using IQCare.CCC.UILogic;
using System.Xml;
using System.Text;
using System;

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
          string jsonObject=  LookupLogic.GeSubCountyListJson(county);
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

            string jsonObject = LookupLogic.GetLookupWardListJson(subcounty);  
            return jsonObject;
        }

        // pw lookup lablist
        [WebMethod]
        public string GetLookupLabsList()
        {
            string jsonObject = LookupLogic.GetLookupLabsListJson();  
            
            //this is what is implemented at UIlogic
            
            return jsonObject;
        }
        // pw .lookup lablist             
        // pw .typeahead 

        // pw lookup previous lablist
        
           [WebMethod]
           public string GetLookupPreviousLabsList(int patientId)
           {
               string jsonObject = LookupLogic.GetLookupPreviousLabsListJson(1);

               //this is what is implemented at UIlogic

               return jsonObject;
           }
           // pw .lookup previous lablist             
           // pw .grid 
           
    }

}
