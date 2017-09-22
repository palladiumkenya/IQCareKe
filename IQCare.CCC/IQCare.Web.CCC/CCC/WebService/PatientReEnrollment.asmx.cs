using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entities.CCC.Triage;
using Entities.CCC.Enrollment;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientReEnrollment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientReEnrollment : System.Web.Services.WebService
    {
        public string msg { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string AddReEnrollment(string reEnrollmentDate)
        {
            try
            {
                var patientReEnrollmentManager = new PatientReEnrollmentManager();
                var patientCareEndingManager = new PatientCareEndingManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();

                int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
                DateTime enrollmentDate = DateTime.Parse(reEnrollmentDate);

                int reEnrollmentId = patientReEnrollmentManager.AddPatientReEnrollment(patientId, enrollmentDate);
                List<PatientCareEnding> careEndings = patientCareEndingManager.GetPatientCareEndings(patientId);
                List<PatientEntityEnrollment> enrollmentList = patientEnrollmentManager.GetPatientByPatientIdCareEnded(patientId);
                if (reEnrollmentId > 0)
                {
                    foreach (var itemCareEnding in careEndings)
                    {
                        itemCareEnding.DeleteFlag = true;
                        itemCareEnding.Active = true;
                        patientCareEndingManager.ResetPatientCareEnding(itemCareEnding);
                    }

                    if (enrollmentList.Count > 0)
                    {
                        enrollmentList[0].CareEnded = false;
                        patientEnrollmentManager.updatePatientEnrollment(enrollmentList[0]);
                    }

                    msg = "Patient has been re-enrolled";
                }
            }
            catch (Exception)
            {
                msg = "Patient re-enrollment failed";
            }

            return msg;
        }
    }
}
