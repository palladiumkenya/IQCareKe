using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic;
using Microsoft.JScript;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientSummaryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientSummaryService : System.Web.Services.WebService
    {
        public string msg { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePatientBio(int patientId, string bioFirstName, string bioMiddleName, string bioLastName, int userId, int bioPatientPopulation)
        {
            int personId = 0;
            int gender = 0;
            try
            {
                bioFirstName = GlobalObject.unescape(bioFirstName);
                bioMiddleName = GlobalObject.unescape(bioMiddleName);
                bioLastName = GlobalObject.unescape(bioLastName);

                var personManager = new PersonManager();
                var patientLogic = new PatientLookupManager();
                var patient = patientLogic.GetPatientDetailSummary(patientId);
                personId = patient[0].PersonId;
                gender = patient[0].Sex;

                personManager.UpdatePerson(bioFirstName, bioMiddleName, bioLastName, gender, userId, personId);
                msg = "<p>Patient Bio Updated Successfully</p>";
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientTreatmentSupporter(string firstName , string middleName , string lastName , string gender , string mobile )
        {
            return msg;
        }
    }
}
