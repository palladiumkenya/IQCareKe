
using System;
using System.Web.Services;
using IQCare.CCC.UILogic;




namespace IQCare.Web.CCC.WebService
{
   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LookupService : System.Web.Services.WebService
    {
        private string _jsonObject;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetLookupSubcountyList(string county)
        {
            Console.WriteLine("GetLookupSubcountyList");
           _jsonObject=  LookupLogic.GeSubCountyListJson(county);
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
            return _jsonObject;
        }
        
        [WebMethod]
        public string GetLookupWardList(string subcounty)
        {
           // Console.WriteLine("GetLookupWardList");
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

        [WebMethod]
        public string GetLookUpItemByName(string itemName)
        {
           // Console.WriteLine("GetLookUpItemByName");
            try
            {
                _jsonObject =LookupLogic.GetLookupItemByName(itemName);    
            }
            catch (Exception e)
            {
                _jsonObject = e.Message;
            }
            return _jsonObject;
        }

        [WebMethod]
        public string GetLookUpItemViewByMasterName(string masterName)
        {
            //Console.WriteLine("GetLookUpItemViewByMasterName");
            try
            {
                _jsonObject = LookupLogic.GetLookUpItemViewByMasterName(masterName);
            }
            catch (Exception e)
            {
                _jsonObject = e.Message;
            }

            return _jsonObject;
        }

        [WebMethod]
        public string GetLookUpItemViewByMasterId(int id)
        {
           // Console.WriteLine("GetLookUpItemViewByMasterId");
            try
            {
                _jsonObject = LookupLogic.GetLookUpItemViewByMasterId(id);
            }
            catch (Exception e)
            {
                _jsonObject = e.Message;
            }

            return _jsonObject;
        }


        // pw lookup lablist
        [WebMethod]
        public string GetLookupLabsList()
        {
            Console.WriteLine("GetLookupLabsList");
            string jsonObject = LookupLogic.GetLookupLabsListJson();

            return jsonObject;
        }

        [WebMethod]
        public string GetCountyByCountyId(int Id)
        {
            if (Id > 0)
            {
                return LookupLogic.GetCountyByCountyId(Id);
            }
            else
                return "";
        }

        [WebMethod]
        public string GetCountyNameBySubCountyId(int subCountyId)
        {
            if (subCountyId > 0)
            {
                return LookupLogic.GetCountyNameBySubCountyId(subCountyId);
            }
            else
                return "";
        }

        [WebMethod]
        public string GetWardNameByWardId(int wardId)
        {
            if (wardId > 0)
            {
                return LookupLogic.GetWardNameByWardId(wardId);
            }
            else
                return "";

        }
    }
}
