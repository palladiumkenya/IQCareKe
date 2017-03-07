using IQCare.CCC.UILogic;
using Entities.CCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;
using Entities.CCC.Visit;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
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
        public string AddPatient(int facilityId, string enrollment, int entryPointId, string enrollmentDate, string personDateOfBirth, string nationalId)
        {
            try
            {
                PersonId = int.Parse(Session["PersonId"].ToString());
                var jss = new JavaScriptSerializer();
                IList<ListEnrollment> data = jss.Deserialize<IList<ListEnrollment>>(enrollment);
                int userId = Convert.ToInt32(Session["AppUserId"]);

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
                        DateOfBirth = DateTime.Parse(personDateOfBirth),
                        NationalId = nationalId,
                        Active = true,
                        CreatedBy = userId,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false
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
                            VisitDate = DateTime.Now,
                            CreatedBy = userId
                        };

                        PatientEntityEnrollment patientEnrollment = new PatientEntityEnrollment
                        {
                            PatientId = patientId,
                            ServiceAreaId = 1,
                            EnrollmentDate = DateTime.Parse(enrollmentDate),
                            CreatedBy = userId,
                            CreateDate = DateTime.Now,
                            DeleteFlag = false
                        };

                        PatientEntryPoint patientEntryPoint = new PatientEntryPoint
                        {
                            PatientId = patientId,
                            ServiceAreaId = 1,
                            EntryPointId = entryPointId,
                            CreatedBy = userId,
                            CreateDate = DateTime.Now,
                            DeleteFlag = false
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
                    var patientLookManager = new PatientLookupManager();
                    List<PatientLookup> patient = patientLookManager.GetPatientByPersonId(PersonId);

                    if (patient.Count > 0)
                    {
                        Session["PatientId"] = patient[0].Id;

                        int patientMasterVisitId = patientMasterVisitManager.PatientMasterVisitCheckin(patient[0].Id, userId);
                        Session["PatientMasterVisitId"] = patientMasterVisitId;

                        List<PatientEntityEnrollment> entityEnrollment = patientEnrollmentManager.GetPatientEnrollmentByPatientId(patient[0].Id);

                        if (entityEnrollment.Count == 0)
                        {
                            PatientEntityEnrollment patientEnrollment = new PatientEntityEnrollment
                            {
                                PatientId = patient[0].Id,
                                ServiceAreaId = 1,
                                EnrollmentDate = DateTime.Parse(enrollmentDate),
                                CreatedBy = userId,
                                CreateDate = DateTime.Now,
                                DeleteFlag = false
                            };

                            PatientEntryPoint patientEntryPoint = new PatientEntryPoint
                            {
                                PatientId = patient[0].Id,
                                ServiceAreaId = 1,
                                EntryPointId = entryPointId,
                                CreatedBy = userId,
                                CreateDate = DateTime.Now,
                                DeleteFlag = false
                            };

                            patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientEnrollment);
                            patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientEntryPoint);

                            if (patientMasterVisitId > 0)
                            {
                                for (int i = 0; i < data.Count; i++)
                                {
                                    /*List<PatientEntityIdentifier> patientEntityIdentifiers = patientIdentifier.GetPatientEntityIdentifiers(patient[0].Id, patientEnrollmentId,
                                        patientEntryPointId);*/

                                    PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                                    {
                                        PatientId = patient[0].Id,
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
                            PatientEntityEnrollment patientEnrollment = new PatientEntityEnrollment
                            {
                                PatientId = patient[0].Id,
                                ServiceAreaId = 1,
                                EnrollmentDate = DateTime.Parse(enrollmentDate),
                                CreatedBy = userId,
                                CreateDate = DateTime.Now,
                                DeleteFlag = false
                            };

                            PatientEntryPoint patientEntryPoint = new PatientEntryPoint
                            {
                                PatientId = patient[0].Id,
                                ServiceAreaId = 1,
                                EntryPointId = entryPointId,
                                CreatedBy = userId,
                                CreateDate = DateTime.Now,
                                DeleteFlag = false
                            };


                            patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientEnrollment);
                            patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientEntryPoint);

                            //Session["PatientMasterVisitId"] = patientMasterVisitId;

                            if (patientMasterVisitId > 0)
                            {
                                for (int i = 0; i < data.Count; i++)
                                {
                                    PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                                    {
                                        PatientId = patient[0].Id,
                                        PatientEnrollmentId = patientEnrollmentId,
                                        IdentifierTypeId = int.Parse(data[i].identifierId),
                                        IdentifierValue = data[i].enrollmentNo
                                    };

                                    patientIdentifierId = patientIdentifier.addPatientIdentifier(patientidentifier);
                                }

                                Msg = "Successfully enrolled patient";
                            }
                        }
                    }

                }
            }
            catch (SoapException ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }

            return Msg;
        }
    }
}
