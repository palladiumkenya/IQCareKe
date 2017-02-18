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
        public string GetPatientSearchx(List<Data> dataPayLoad)
        {
            String output=null;
            Utility utility=new Utility();
            try
            {
                PatientLookupManager patientLookup=new PatientLookupManager();
                var jsonData = patientLookup.GetPatientSearchListPayload();

                if (jsonData.Count > 0)
                {
                    var sEcho = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "sEcho").value);
                    var displayLength = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayLength").value);
                    var displayStart = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayStart").value);
                    var patientId = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "patientId").value);
                    var firstName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "firstName").value);
                    var middleName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "middleName").value);
                    var lastName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "lastName").value);
                   // var dateOfBirth = Convert.ToDateTime(dataPayLoad.FirstOrDefault(x => x.name == "DateOfBirth").value);
                   // var gender = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "gender").value);
                    var facility = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "facility").value);

                    if (!string.IsNullOrWhiteSpace(patientId))
                    {
                        jsonData = jsonData.Where(x=>x.EnrollmentNumber==patientId).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        jsonData = jsonData.Where(x => x.FirstName == firstName).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(lastName))
                    {
                        jsonData = jsonData.Where(x => utility.Decrypt(x.LastName).ToLower().Contains(lastName.ToLower())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(middleName))
                    {
                        jsonData = jsonData.Where(x => utility.Decrypt(x.MiddleName).ToLower().Contains(middleName.ToLower())).ToList();
                    }

                    /*---- Perform paging based on request */
                    var skip = (displayLength * displayStart);
                    var ableToSkip = skip < displayLength;
                    string patientStatus;
                    jsonData = jsonData.Skip(skip).Take(displayLength).ToList();

                    var json = new
                    {

                        draw = sEcho,
                        recordsTotal = jsonData.Count,
                        recordsFiltered = jsonData.Count,
                      
                        data = jsonData.Select(x => new string[]
                        {
                            
                            x.Id.ToString(),
                            x.EnrollmentNumber.ToString(),
                            utility.Decrypt(x.FirstName),
                            utility.Decrypt(x.MiddleName),
                            utility.Decrypt(x.LastName),
                            x.DateOfBirth.ToString("dd-MMM-yyyy"),
                            LookupLogic.GetLookupNameById(x.Sex),
                            x.RegistrationDate.ToString("dd-MMM-yyyy"),
                            x.PatientStatus.ToString()
                        })
                    };
                    output = JsonConvert.SerializeObject(json);
                }         
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                output = e.Message + ' ' + e.InnerException;
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
            /*set util function to decrypt*/
            Utility utility=new Utility();

            /* Grab values from aoData object sent by datatables */
            dynamic patientList = null;
            var sEcho =Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "sEcho").value);
            var displayLength = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayLength").value);
            var displayStart = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayStart").value);
            var patientId=Convert.ToString(dataPayLoad.FirstOrDefault(x=>x.name=="patientId").value);
            var firstName=Convert.ToString(dataPayLoad.FirstOrDefault(x=>x.name== "firstName").value);
            var middleName=Convert.ToString(dataPayLoad.FirstOrDefault(x=>x.name== "middleName").value);
            var lastName=Convert.ToString(dataPayLoad.FirstOrDefault(x=>x.name== "lastName").value);
            var dateOfBirth=Convert.ToDateTime(dataPayLoad.FirstOrDefault(x=>x.name== "DateOfBirth").value);
            var gender=Convert.ToInt32(dataPayLoad.FirstOrDefault(x=>x.name== "gender").value);
            var facility=Convert.ToInt32(dataPayLoad.FirstOrDefault(x=>x.name== "facility").value);
            //var registrationDate =Convert.ToDateTime(dataPayLoad.FirstOrDefault(x => x.name == "registrationDate").value);


            try
            {
                PatientLookupManager patientLookup=new PatientLookupManager();
                var patientLookups = patientLookup.GetPatientSearchPayloadWithParameter(patientId, firstName, middleName,
                    lastName, dateOfBirth, gender, facility, displayStart,displayLength);

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
                            x.Sex.ToString(),
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
