using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Application.Common;
using IQCare.CCC.UILogic;
using Newtonsoft.Json;


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

        //[WebMethod]
        //public string PatientFinder()
        //{
        //    string patientList = null;

        //    try
        //    {
        //        PatientLookupManager patientLookup = new PatientLookupManager();
        //        var patientLookups = patientLookup.GetPatientSearchListPayload().ToList();

        //        if (patientLookups.Count > 0)
        //        {
        //            var json = new
        //            {

        //                draw = 1,
        //                recordsTotal = 1, // Convert.ToInt32(patientLookups.Count()),
        //                recordsFiltered = 1, // Convert.ToInt32(patientLookups.Count()),
        //                data = patientLookups.Select(x => new string[]
        //                {
        //                    x.Id.ToString(),
        //                    x.PatientIndex.ToString(),
        //                    x.FirstName,
        //                    x.MiddleName,
        //                    x.LastName,
        //                    x.DateOfBirth.ToShortDateString(),
        //                    x.Sex.ToString(),
        //                    x.RegistrationDate.ToShortDateString(),
        //                    x.PatientStatus.ToString()
        //                })
        //            };
        //            patientList = json.ToString();
        //        }
        //    }
        //    catch (Exception var )
        //    {
        //        Console.WriteLine(var );
        //        throw;
        //    }

        //    return JsonConvert.SerializeObject(patientList);
        //}
        

        [WebMethod]
        public string FindPatient(List<Data> dataPayLoad)
        {
           // var request = HttpContext.Current.Request;
            int sEcho = 0;int displayStart = 0;int displayLength = 0;
            dynamic patientList = null;
            
            Utility utility=new Utility();

            var c = dataPayLoad.FirstOrDefault(x => x.name == "sEcho").value;
            var dl = dataPayLoad.FirstOrDefault(x => x.name == "iDisplayLength").value;
            var ds = dataPayLoad.FirstOrDefault(x => x.name == "iDisplayStart").value;

            /* search parameters */

            if (Convert.ToInt32(c) > 0){ sEcho = Convert.ToInt32(c);}
            if (Convert.ToInt32(dl) > 0){ displayLength = Convert.ToInt32(dl);}
            if (Convert.ToInt32(ds) > 0){ displayStart = Convert.ToInt32(ds); }

            try
            {
                PatientLookupManager patientLookup=new PatientLookupManager();
                var patientLookups= patientLookup.GetPatientSearchListPayload().ToList();

                if (patientLookups.Count>0)
                {
                    var json = new 
                    {

                        draw = sEcho,
                        recordsTotal = Convert.ToInt32(patientLookups.Count()),
                        recordsFiltered = Convert.ToInt32(patientLookups.Count()),
                        data = patientLookups.Select(x => new string[]
                        {
                            x.Id.ToString(),
                            x.EnrollmentNumber,
                            utility.Decrypt(x.FirstName),
                            utility.Decrypt(x.MiddleName),
                            utility.Decrypt(x.LastName),
                            x.DateOfBirth.ToString("MMM-dd-yyyy"),
                            LookupLogic.GetLookupNameById(x.Sex),
                            x.RegistrationDate.ToString("MMM-dd-yyyy"),
                            x.PatientStatus.ToString()
                        })
                    };
                    patientList= json;
                }
                return JsonConvert.SerializeObject(patientList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
