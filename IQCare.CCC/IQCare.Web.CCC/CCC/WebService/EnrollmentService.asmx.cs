using IQCare.CCC.UILogic;
using Entities.CCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using Entities.CCC.Visit;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Enrollment;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.WebService
{
    public class ListEnrollment
    {
        public string enrollmentIdentifier { get; set; }
        public string identifierId { get; set; }
        public string enrollmentNo { get; set; }
    }
    /// <summary>
    /// Summary description for EnrollmentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EnrollmentService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int patientId { get; set; }
        private int patientMasterVisitId { get; set; }
        private int patientEnrollmentId { get; set; }
        private int patientIdentifierId { get; set; }
        private int patientEntryPointId { get; set; }
        private int PersonId { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string AddPatient(int personid, int facilityId, string enrollment, int entryPointId, string enrollmentDate)
        {
            try
            {
                PersonId = int.Parse(Session["PersonId"].ToString());
                var jss = new JavaScriptSerializer();
                IList<ListEnrollment> data = jss.Deserialize<IList<ListEnrollment>>(enrollment);
                
                var patientManager = new PatientManager();
                var patientMasterVisitManager = new PatientMasterVisitManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();
                var patientIdentifier = new PatientIdentifierManager();
                var patientEntryPointManager = new PatientEntryPointManager();

                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                int isPersonEnrolled = patientManager.CheckPersonEnrolled(PersonId).Count;

                if (isPersonEnrolled == 0)
                {

                    PatientEntity patient = new PatientEntity
                    {
                        PersonId = PersonId,
                        ptn_pk = 0,
                        FacilityId = facilityId,
                        PatientIndex = datevalue.Year.ToString() + '-' + PersonId,
                        Active = true
                    };

                    patientId = patientManager.AddPatient(patient);
                    Session["PatientId"] = patientId;

                    if (patientId > 0)
                    {
                        PatientMasterVisit visit = new PatientMasterVisit
                        {
                            PatientId = patientId,
                            ServiceId = 1,
                            Start = DateTime.Now,
                            Active = true,
                            CreateDate = DateTime.Now,
                            DeleteFlag = false,
                            VisitDate = DateTime.Now
                        };

                        PatientEntityEnrollment patientEnrollment = new PatientEntityEnrollment
                        {
                            PatientId = patientId,
                            ServiceAreaId = 1,
                            EnrollmentDate = DateTime.Parse(enrollmentDate)
                        };

                        PatientEntryPoint patientEntryPoint = new PatientEntryPoint
                        {
                            PatientId = patientId,
                            ServiceAreaId = 1,
                            EntryPointId = entryPointId
                        };

                        patientMasterVisitId = patientMasterVisitManager.AddPatientMasterVisit(visit);
                        patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientEnrollment);
                        patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientEntryPoint);

                        Session["PatientMasterVisitId"] = patientMasterVisitId;

                        if (patientMasterVisitId > 0)
                        {
                            for (int i = 0; i < data.Count; i++)
                            {
                                PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                                {
                                    PatientId = patientId,
                                    PatientEnrollmentId = patientEnrollmentId,
                                    IdentifierTypeId = int.Parse(data[i].identifierId),
                                    IdentifierValue = data[i].enrollmentNo
                                };

                                patientIdentifierId = patientIdentifier.addPatientIdentifier(patientidentifier);
                            }

                            Msg = "Successfully enrolled patient";
                        }

                    }
                    else
                    {
                        Msg = " Error occurred in enrollment ";
                    }
                }
                else
                {
                    Msg = " The person is alreay enrolled";
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }

            return Msg;
        }
    }
}
