using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using IQCare.CCC.UILogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPatientSearchx()
        {
            String output;

            try
            {
                PatientLookupManager patientLookup=new PatientLookupManager();
                var jsonData = patientLookup.GetPatientSearchListPayload();
                output= JsonConvert.SerializeObject(jsonData);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return output;
        }

        [WebMethod]
        public string FindPatient(List<Data> data)
        {
           // var request = HttpContext.Current.Request;
            int sEcho = 0;int displayStart = 0;int displayLength = 0;


            var c = data.FirstOrDefault(x => x.name == "sEcho").value;
            var dl = data.FirstOrDefault(x => x.name == "iDisplayLength").value;
            var ds = data.FirstOrDefault(x => x.name == "iDisplayStart").value;

            /* search parameters */


            if (sEcho > 0){ sEcho = Convert.ToInt32(sEcho);}
            if (Convert.ToInt32(dl) > 0){ displayLength = Convert.ToInt32(dl);}
            if (Convert.ToInt32(ds) > 0){ displayStart = Convert.ToInt32(ds); }

            //string jsonObject="steve";
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return sEcho.ToString();
            //  ClassName ObjectName = JsonConvert.DeserializeObject<ClassName>(jsonObject);


        }
    }
    public class Data
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Data1
    {
        public List<Data> data { get; set; }
    }
}
