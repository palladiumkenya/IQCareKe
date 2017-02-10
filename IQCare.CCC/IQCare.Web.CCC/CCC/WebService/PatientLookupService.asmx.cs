using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Application.Presentation;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
//using Newtonsoft.Json;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientLookupService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientLookupService : System.Web.Services.WebService
    {


        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPatientSearch()
        {

            dynamic jsonData = null;
            var echo = Convert.ToInt32(HttpContext.Current.Request.Params["sEcho"]);
            var displayLength = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);
            var displayStart = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            var sortOrder = Convert.ToString(HttpContext.Current.Request.Params["sSortDir_0"]);
            var totalRecords = 0;
            var totalFiltered = 0;

            try
            {
                IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

                var patientLookup = new PatientLookupManager();
                var foundPatient = patientLookup.GetPatientSearchListPayload();

                if (foundPatient.Count > 0)
                {
                    totalFiltered = Convert.ToInt32(foundPatient.Count);
                    totalRecords = Convert.ToInt32(foundPatient.Count);

                    dynamic jsnonData = new
                    {
                        draw= HttpContext.Current.Request.Params["draw"],
                        recordsTotal= totalRecords,
                        recordsFiltered=totalFiltered,
                        data = foundPatient
                    };

                    //jsonData = JsonConvert.SerializeObject(jsnonData);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Context.Response.ContentType = "application/json; charset=utf-8";
            return new JavaScriptSerializer().Serialize(jsonData);
        }
    }
}
