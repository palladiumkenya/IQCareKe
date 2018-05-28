using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Application.Common;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic;
using System.Web.Script.Serialization;

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
            int filteredRecords = 0;
            int totalCount = 0;
            


            var jsonData = new List<PatientLookup>();

            try
            {
                string patientId ="";
                string firstName = null;
                string middleName = null;
                string lastName = null;
                string isEnrolled = null;

                PatientLookupManager patientLookup = new PatientLookupManager();


                patientId = dataPayLoad.FirstOrDefault(x => x.name == "patientId").value;
                firstName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "firstName").value);
                middleName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "middleName").value);
                lastName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "lastName").value);
                isEnrolled = Convert.ToString(dataPayLoad.FirstOrDefault(x=>x.name== "isEnrolled").value);

                //patientId = patientId != "" ? patientId : 0;

                if (patientId !="" || !string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(middleName) || !string.IsNullOrWhiteSpace(lastName))
                {
                    jsonData = patientLookup.GetPatientSearchListPayload(patientId,isEnrolled, firstName, middleName, lastName);
                }
                else
                {
                    jsonData = patientLookup.GetPatientSearchListPayload(isEnrolled);
                }

                if (jsonData.Count > 0)
                {
                    var sEcho = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "sEcho").value);
                    var displayLength = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayLength").value);
                    var displayStart = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iDisplayStart").value);
                    
                    
                    // var dateOfBirth = Convert.ToDateTime(dataPayLoad.FirstOrDefault(x => x.name == "DateOfBirth").value);
                    // var gender = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "gender").value);
                    var facility = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "facility").value);

                    var sortCol = Convert.ToInt32(dataPayLoad.FirstOrDefault(x => x.name == "iSortCol_0").value);
                    string sortDir = dataPayLoad.FirstOrDefault(x => x.name == "sSortDir_0").value;
                    string searchString = dataPayLoad.FirstOrDefault(x => x.name == "sSearch").value;


                    //if (!string.IsNullOrWhiteSpace(patientId))
                    //{
                    //    //jsonData = jsonData.Where(x => x.EnrollmentNumber == patientId).ToList();
                    //    jsonData = jsonData.Where(x => x.EnrollmentNumber.Contains(patientId)).ToList();
                    //}

                    //if (!string.IsNullOrWhiteSpace(firstName))
                    //{
                    //    jsonData =
                    //        jsonData.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()))
                    //            .ToList();
                    //}
                    //if (!string.IsNullOrWhiteSpace(lastName))
                    //{
                    //    jsonData =
                    //        jsonData.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()))
                    //            .ToList();
                    //}
                    //if (!string.IsNullOrWhiteSpace(middleName))
                    //{
                    //    jsonData =
                    //        jsonData.Where(x => x.MiddleName.ToLower().Contains(middleName.ToLower()))
                    //            .ToList();
                    //}

                    /*-- order columns based on payload received -- */
                    switch (sortCol)
                    {
                        case 0:
                            jsonData = (sortDir == "desc")
                                ? jsonData = jsonData.OrderByDescending(x => x.Id).ToList()
                                : jsonData.OrderBy(x => x.Id).ToList();

                            break;
                        case 1:
                            jsonData = (sortDir == "desc")
                                ? jsonData = jsonData.OrderByDescending(x => x.EnrollmentNumber).ToList()
                                : jsonData.OrderBy(x => x.EnrollmentNumber).ToList();
                            break;
                        case 2:
                            jsonData = (sortDir == "desc")
                                ? jsonData = jsonData.OrderByDescending(x => x.FirstName).ToList()
                                : jsonData.OrderBy(x => x.FirstName).ToList();
                            break;
                        case 3:
                            jsonData = (sortDir == "desc")
                                ? jsonData = jsonData.OrderByDescending(x => x.MiddleName).ToList()
                                : jsonData.OrderBy(x => x.MiddleName).ToList();
                            break;
                        case 4:
                            jsonData = (sortDir == "desc")
                                ? jsonData.OrderBy(x => x.LastName).ToList()
                                : jsonData = jsonData.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case 5:
                            jsonData = (sortDir == "desc")
                                ? jsonData.OrderBy(x => x.DateOfBirth).ToList()
                                : jsonData = jsonData.OrderByDescending(x => x.DateOfBirth).ToList();
                            break;
                        case 6:
                            jsonData = (sortDir == "desc")
                                ? jsonData.OrderBy(x => LookupLogic.GetLookupNameById(x.Sex)).ToList()
                                : jsonData =
                                    jsonData.OrderByDescending(x => LookupLogic.GetLookupNameById(x.Sex)).ToList();
                            break;
                        case 7:
                            jsonData = (sortDir == "desc")
                                ? jsonData = jsonData.OrderByDescending(x => x.EnrollmentDate).ToList()
                                : jsonData.OrderBy(x => x.EnrollmentDate).ToList();
                            break;
                        case 8:
                            break;
                    }

                    /*-- implement search -- */
                    if (searchString.Length > 0 || !string.IsNullOrWhiteSpace(searchString))
                    {
                        jsonData = jsonData.Where(x => x.EnrollmentNumber.Equals(searchString) ||
                                                       x.FirstName
                                                           .ToLower()
                                                           .Contains(searchString.ToLower()) ||
                                                       x.MiddleName
                                                           .ToLower()
                                                           .Contains(searchString.ToLower()) ||
                                                       x.LastName
                                                           .ToLower()
                                                           .Contains(searchString.ToLower()) ||
                                                       LookupLogic.GetLookupNameById(x.Sex)
                                                           .Contains(searchString.ToLower()) ||
                                                       x.EnrollmentNumber.Contains(searchString.ToString()) ||
                                                       x.MobileNumber.Contains(searchString)
                            )
                            .ToList();
                        filteredRecords = jsonData.Count();
                    }
                    else
                    {
                        filteredRecords = jsonData.Count();
                    }

                    /*---- Perform paging based on request */
                    //  var skip = (displayLength * displayStart);
                    // var ableToSkip = skip < displayLength;
                    //string patientStatus;
                    totalCount = jsonData.Count();
                    jsonData = jsonData.Skip(displayStart).Take(displayLength).ToList();

                    var json = new
                    {
                        draw = sEcho,
                        recordsTotal = totalCount,
                        recordsFiltered = filteredRecords,
                        // recordsFiltered =(filteredRecords>0)?filteredRecords: jsonData.Count(),

                        data = jsonData.Select(x => new string[]
                        {

                            (isEnrolled=="notEnrolledClients")? x.PersonId.ToString(): x.Id.ToString(),
                            x.EnrollmentNumber.ToString(),
                            x.FirstName,
                            x.MiddleName,
                            x.LastName,
                            x.DateOfBirth.ToString("dd-MMM-yyyy"),
                            LookupLogic.GetLookupNameById(x.Sex),
                            //x.RegistrationDate.ToString("dd-MMM-yyyy"),
                            (isEnrolled=="notEnrolledClients")? Convert.ToDateTime(x.RegistrationDate).ToString("dd-MMM-yyyy") : x.EnrollmentDate.ToString("dd-MMM-yyyy"),
                            x.PatientStatus.ToString()
                            //,utility.Decrypt(x.MobileNumber)
                        })
                    };

                    //output = JsonConvert.SerializeObject(json);
                    output = new JavaScriptSerializer().Serialize(json);
                }
            }
            catch (Exception e)
            {
                //Dispose();
                output = e.Message;
            }
            finally
            {
                Dispose();
            }
            return output;
        }

        
        

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
                            x.FirstName,
                            x.MiddleName,
                            x.LastName,
                            x.DateOfBirth.ToString("MMM-dd-yyyy"),
                            x.Sex.ToString(),
                            x.EnrollmentDate.ToString("MMM-dd-yyyy"),
                            x.PatientStatus.ToString(),
                            x.MobileNumber.ToString()
                        })
                    };
                    patientList= json;
                }

                //return JsonConvert.SerializeObject(patientList);
                return new JavaScriptSerializer().Serialize(patientList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientFamilyMembers(string firstName, string middleName, string lastName, string sex)
        {
            List<PatientLookup> patientLookups = new List<PatientLookup>();
            IEnumerable<object> newresults = Enumerable.Empty<object>();
            try
            {
                LookupLogic logic = new LookupLogic();
                int intSex = 0;
                if (!System.String.IsNullOrEmpty(sex))
                {
                    var items = logic.GetItemIdByGroupAndItemName("Gender", sex);
                    if (items.Count > 0)
                    {
                        intSex = items[0].ItemId;
                    }
                }

                int patientId = int.Parse(Session["PatientPK"].ToString());

                PatientLookupManager patientLookup = new PatientLookupManager();
                PersonRelationshipManager relationManager = new PersonRelationshipManager();
                patientLookups = patientLookup.GetPatientListByParams(patientId, firstName, middleName, lastName, intSex);

                newresults = patientLookups.Select(x => new Dictionary<string, string>()
                {
                    {
                        "Id",x.Id.ToString()
                    },
                    {
                        "EnrollmentNumber",x.EnrollmentNumber.ToString()
                    },
                    {
                        "FirstName",x.FirstName
                    },
                    {
                        "MiddleName",x.MiddleName
                    },
                    {
                        "LastName",x.LastName
                    },
                    {
                        "DateOfBirth",x.DateOfBirth.ToString()
                    },
                    {
                        "Sex",LookupLogic.GetLookupNameById(x.Sex)
                    },
                    {
                        "EnrollmentDate",x.EnrollmentDate.ToString()
                    },
                    {
                        "PatientStatus",x.PatientStatus
                    },
                    {
                        "PersonId",x.PersonId.ToString()
                    },
                    {
                        "LinkedToPatient",relationManager.PersonLinkedToPatient(x.PersonId, patientId).ToString()
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //return JsonConvert.SerializeObject(newresults);
            return new JavaScriptSerializer().Serialize(newresults);
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
