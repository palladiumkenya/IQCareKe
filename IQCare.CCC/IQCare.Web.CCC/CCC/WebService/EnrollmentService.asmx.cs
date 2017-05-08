using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;
using Application.Common;
using Entities.CCC.Visit;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Enrollment;
using Microsoft.JScript;
using Convert = System.Convert;

namespace IQCare.Web.CCC.WebService
{
    public class CareEndingDetails
    {
        public DateTime ExitDate { get; set; }
        public string ExitReason { get; set; }
        public bool Status { get; set; }
    }
    public class ListEnrollment
    {
        public string enrollmentIdentifier { get; set; }
        public string identifierId { get; set; }
        public string enrollmentNo { get; set; }
    }

    public class ExMessage
    {
        public string msg { get; set; }
        public int errorcode { get; set; }
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
        private int ptn_Pk { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string AddPatient(int facilityId, string enrollment, int entryPointId, string enrollmentDate, string personDateOfBirth, string nationalId, int patientType, string dobPrecision)
        {
            ExMessage message = new ExMessage();
            try
            {
                
                Utility utility = new Utility();
                PersonId = int.Parse(Session["PersonId"].ToString());
                var jss = new JavaScriptSerializer();
                IList<ListEnrollment> data = jss.Deserialize<IList<ListEnrollment>>(enrollment);
                int userId = Convert.ToInt32(Session["AppUserId"]);

                var patientManager = new PatientManager();
                var patientMasterVisitManager = new PatientMasterVisitManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();
                var patientIdentifierManager = new PatientIdentifierManager();
                var patientEntryPointManager = new PatientEntryPointManager();
                var patientLookUpManager = new PatientLookupManager();
                var mstPatientLogic = new MstPatientLogic();
                var personContactLookUpManager = new PersonContactLookUpManager();
                var personContacts = new List<PersonContactLookUp>();
                var personLookUp = new PersonLookUpManager();
                var lookupLogic = new LookupLogic();



                String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);

                for(int i = 0; i < data.Count; i++)
                {
                    var identifierTypeId = int.Parse(data[i].identifierId);
                    var identifierValue = data[i].enrollmentNo;

                    var identifiers = patientIdentifierManager.CheckIfIdentifierNumberIsUsed(identifierValue, identifierTypeId);

                    if (identifiers.Count > 0)
                    {
                        var exception = new SoapException("No: " + identifierValue + " already exists", SoapException.ClientFaultCode);
                        throw exception;
                    }
                }

                int isPersonEnrolled = patientLookUpManager.GetPatientByPersonId(PersonId).Count;

                if (isPersonEnrolled == 0)
                {

                    PatientEntity patient = new PatientEntity
                    {
                        PersonId = PersonId,
                        ptn_pk = 0,
                        FacilityId = facilityId,
                        PatientType = patientType,
                        PatientIndex = datevalue.Year.ToString() + '-' + PersonId,
                        DateOfBirth = DateTime.Parse(personDateOfBirth),
                        NationalId =(nationalId),
                        Active = true,
                        CreatedBy = userId,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        DobPrecision = bool.Parse(dobPrecision)
                    };

                    patientId = patientManager.AddPatient(patient);
                    Session["PatientId"] = patientId;

                    if (patientId > 0)
                    {
                        var visitTypes = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment");
                        var visitType = 0;
                        if (visitTypes.Count > 0)
                        {
                            visitType = visitTypes[0].ItemId;
                        }

                        //
                        PatientMasterVisit visit = new PatientMasterVisit
                        {
                            PatientId = patientId,
                            ServiceId = 1,
                            Start = DateTime.Now,
                            Active = true,
                            CreateDate = DateTime.Now,
                            DeleteFlag = false,
                            VisitDate = DateTime.Now,
                            CreatedBy = userId,
                            VisitType = visitType
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
                        var patient_person_details = personLookUp.GetPersonById(PersonId);



                        if (patient_person_details != null)
                        {
                            var maritalStatus = new PersonMaritalStatusManager().GetCurrentPatientMaritalStatus(PersonId);
                            personContacts = personContactLookUpManager.GetPersonContactByPersonId(PersonId);
                            var address = "";
                            var phone = "";
                            var facility = lookupLogic.GetFacility();

                            if (personContacts.Count > 0)
                            {
                                address = personContacts[0].PhysicalAddress;
                                phone = personContacts[0].MobileNumber;
                            }

                            var MaritalStatusId = 0;
                            if (maritalStatus != null)
                            {
                                MaritalStatusId = maritalStatus.MaritalStatusId;
                            }

                            var sex = 0;
                            var enrollmentBlueCardId = "";

                            if (LookupLogic.GetLookupNameById(patient_person_details.Sex) == "Male")
                            {
                                sex = 16;
                            }else if (LookupLogic.GetLookupNameById(patient_person_details.Sex) == "Female")
                            {
                                sex = 17;
                            }

                            foreach (var item in data)
                            {
                                if (item.identifierId == "1248")
                                {
                                    enrollmentBlueCardId = item.enrollmentNo;
                                }
                            }



                            ptn_Pk = mstPatientLogic.InsertMstPatient(
                                (patient_person_details.FirstName), 
                                (patient_person_details.LastName),
                                (patient_person_details.MiddleName),
                                facility.FacilityID, enrollmentBlueCardId, entryPointId,
                                patientEnrollment.EnrollmentDate, sex,
                                patient.DateOfBirth,
                                1, MaritalStatusId,
                                address, phone, userId, Session["AppPosID"].ToString(),
                                203, patientEnrollment.EnrollmentDate, DateTime.Now);

                            patient.ptn_pk = ptn_Pk;
                            patientManager.UpdatePatient(patient, patientId);
                        }

                        


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

                                patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientidentifier);
                                mstPatientLogic.AddOrdVisit(ptn_Pk, facilityId, visit.Start, patientIdentifierId, userId, DateTime.Now, 203);
                            }

                            message.errorcode = 0;
                            message.msg += "<p>Successfully enrolled patient.</p>";
                        }

                    }
                    else
                    {
                        message.errorcode = 1;
                        message.msg += "<p>Error occurred in enrollment.</p>";
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
                                    PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                                    {
                                        PatientId = patient[0].Id,
                                        PatientEnrollmentId = patientEnrollmentId,
                                        IdentifierTypeId = int.Parse(data[i].identifierId),
                                        IdentifierValue = data[i].enrollmentNo
                                    };

                                    patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientidentifier);
                                }      

                                message.errorcode = 0;
                                message.msg += "<p>Successfully enrolled patient.</p>";
                            }
                        }
                        else
                        {
                            if (patientMasterVisitId > 0)
                            {
                                for (int i = 0; i < data.Count; i++)
                                {
                                    List<PatientEntityIdentifier> patientEntityIdentifiers = patientIdentifierManager.GetPatientEntityIdentifiers(patient[0].Id, entityEnrollment[0].Id,
                                        int.Parse(data[i].identifierId));
                                    if (patientEntityIdentifiers.Count > 0)
                                    {
                                        Msg += "<p>" + data[i].enrollmentIdentifier + " is already enrolled.</p>";
                                    }
                                    else
                                    {
                                        PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                                        {
                                            PatientId = patient[0].Id,
                                            PatientEnrollmentId = entityEnrollment[0].Id,
                                            IdentifierTypeId = int.Parse(data[i].identifierId),
                                            IdentifierValue = data[i].enrollmentNo
                                        };

                                        patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientidentifier);
                                    }
                                }

                                message.errorcode = 0;
                                message.msg += "<p>Successfully enrolled patient.</p>";
                            }
                        }
                    }

                }
            }
            catch (SoapException ex)
            {
                message.errorcode = 1;
                message.msg = ex.Message;
                
            }

            return Msg = new JavaScriptSerializer().Serialize(message);
        }

        [WebMethod(EnableSession = true)]
        public string EndPatientCare(string exitDate, int exitReason,string facilityOutTransfer,string dateOfDeath, string careEndingNotes)
        {
            try
            {
                PatientCareEndingManager careEndingManager = new PatientCareEndingManager();
                PatientEnrollmentManager enrollmentManager = new PatientEnrollmentManager();

                patientId = int.Parse(Session["PatientId"].ToString());
                patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());
                var enrollments = enrollmentManager.GetPatientEnrollmentByPatientId(patientId);
                if (enrollments.Count > 0)
                    patientEnrollmentId = enrollments[0].Id;

                if (patientEnrollmentId > 0)
                {
                    if (!String.IsNullOrWhiteSpace(facilityOutTransfer))
                    {
                        careEndingManager.AddPatientCareEndingTransferOut(patientId, patientMasterVisitId,
                            patientEnrollmentId,
                            exitReason, DateTime.Parse(exitDate), GlobalObject.unescape(facilityOutTransfer),
                            GlobalObject.unescape(careEndingNotes));
                    }
                    else
                        careEndingManager.AddPatientCareEndingDeath(patientId, patientMasterVisitId, patientEnrollmentId,
                        exitReason, DateTime.Parse(exitDate), DateTime.Parse(dateOfDeath),  GlobalObject.unescape(careEndingNotes));

                    PatientEntityEnrollment entityEnrollment =
                        enrollmentManager.GetPatientEntityEnrollment(patientEnrollmentId);
                    entityEnrollment.CareEnded = true;
                    enrollmentManager.updatePatientEnrollment(entityEnrollment);

                    Msg = "Patient has been successfully care ended";
                }
                else
                {
                    SoapException b = new SoapException();
                    SoapException e = (SoapException)Activator.CreateInstance(b.GetType(), "Patient is already care ended", b);
                    Msg = e.Message;
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public List<CareEndingDetails> GetPatientCareEnded()
        {
            PatientCareEndingManager careEndingManager = new PatientCareEndingManager();
            List<CareEndingDetails> careEndingDetailses = new List<CareEndingDetails>();

            patientId = int.Parse(Session["PatientId"].ToString());
            var careEndings = careEndingManager.GetPatientCareEndings(patientId);
            if (careEndings.Count > 0)
            {
                foreach (var item in careEndings)
                {
                    CareEndingDetails careEndingDetails = new CareEndingDetails();
                    careEndingDetails.ExitDate = item.ExitDate;
                    careEndingDetails.ExitReason = LookupLogic.GetLookupNameById(item.ExitReason);
                    careEndingDetails.Status = item.Active;

                    careEndingDetailses.Add(careEndingDetails);
                }
                
            }
            return careEndingDetailses;
        }

        [WebMethod(EnableSession = true)]
        public List<PatientServiceEnrollmentLookup> GetPatientEnrollments()
        {
            try
            {
                PersonId = int.Parse(Session["PersonId"].ToString());
                var patientServiceEnrollment = new PatientServiceEnrollmentLookupManager();
                var patientEnrollments = patientServiceEnrollment.GetPatientServiceEnrollments(PersonId);
                return patientEnrollments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
