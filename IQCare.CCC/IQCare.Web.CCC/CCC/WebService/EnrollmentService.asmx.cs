using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Entities.CCC.Visit;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.AuditDataUtility;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Visit;
using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Convert = System.Convert;

namespace IQCare.Web.CCC.WebService
{
    public class FormDetails
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string DataType { get; set; }
        public bool Required { get; set; }
        public string Prefix { get; set; }
        public string SuffixType { get; set; }
        public string Code { get; set; }
        public string IdentifierName { get; set; }
    }
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

    public class EnrollmentDetails
    {
        public string DOB { get; set; }
        public bool DobPrecision { get; set; }
        public string NationalId { get; set; }
        public string EnrollmentDate { get; set; }
        public int EntryPointId { get; set; }
        public List<Identifier> IndentifiersList { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string EnrollmentValue { get; set; }
        public bool EntryPointIdUnknown { get; set; }
    }

    public class IdentifierList
    {
        public int Id { get; set; }
        public string Value { get; set; }
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

        private readonly IPatientLinkageManager linkageManager = (IPatientLinkageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientLinkageManager, BusinessProcess.CCC");
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string AddPatient(int facilityId, int entryPointId, string enrollmentDate, string personDateOfBirth, string nationalId, int patientType, string dobPrecision, string identifiersList)
        {
            ExMessage message = new ExMessage();
            try
            {
                PersonId = int.Parse(Session["PersonId"].ToString());
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

                var identifiersObjects = JsonConvert.DeserializeObject<Dictionary<int, string>>(identifiersList);
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                List<PatientLookup> isPersonEnrolled = patientLookUpManager.GetPatientByPersonId(PersonId);

                foreach (var item in identifiersObjects)
                {
                    var identifiers = patientIdentifierManager.CheckIfIdentifierNumberIsUsed(item.Value, item.Key);
                    if (identifiers.Count > 0)
                    {
                        foreach (var items in identifiers)
                        {
                            if (isPersonEnrolled.Count > 0)
                            {
                                if (items.PatientId == isPersonEnrolled[0].Id)
                                {

                                }
                                else
                                {
                                    var exception = new SoapException("No: " + item.Value + " already exists", SoapException.ClientFaultCode);
                                    throw exception;
                                }
                            }
                            else
                            {
                                var exception = new SoapException("No: " + item.Value + " already exists", SoapException.ClientFaultCode);
                                throw exception;
                            }
                        }
                    }
                }

                if (isPersonEnrolled.Count == 0)
                {
                    List<PatientRegistrationLookup> patientsByPersonId = patientManager.GetPatientIdByPersonId(PersonId);
                    var patientIndex = datevalue.Year.ToString() + '-' + PersonId;
                    PatientEntity patient = new PatientEntity();
                    if (patientsByPersonId.Count > 0)
                    {
                        patient.FacilityId = facilityId;
                        patient.DateOfBirth = DateTime.Parse(personDateOfBirth);
                        patient.NationalId = nationalId;
                        patient.ptn_pk = patientsByPersonId[0].ptn_pk > 0 ? patientsByPersonId[0].ptn_pk : 0;

                        patientManager.UpdatePatient(patient, patientsByPersonId[0].Id);
                        patientId = patientsByPersonId[0].Id;
                    }
                    else
                    {
                        patient.PersonId = PersonId;
                        patient.ptn_pk = 0;
                        patient.FacilityId = facilityId;
                        patient.PatientType = patientType;
                        patient.PatientIndex = patientIndex;
                        patient.DateOfBirth = DateTime.Parse(personDateOfBirth);
                        patient.NationalId = (nationalId);
                        patient.Active = true;
                        patient.CreatedBy = userId;
                        patient.CreateDate = DateTime.Now;
                        patient.DeleteFlag = false;
                        patient.DobPrecision = bool.Parse(dobPrecision);

                        patientId = patientManager.AddPatient(patient);
                    }
                    Session["PatientPK"] = patientId;

                    if (patientId > 0)
                    {
                        var visitTypes = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment");
                        var visitType = 0;
                        if (visitTypes.Count > 0)
                        {
                            visitType = visitTypes[0].ItemId;
                        }

                        //Add enrollment visit
                        patientMasterVisitId =
                            patientMasterVisitManager.AddPatientMasterVisit(patientId, userId, visitType);
                        //Enroll Patient to service
                        patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, enrollmentDate, userId);
                        //Add enrollment entry point
                        patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, userId);

                        //Get User Details to be used in BLUE CARD
                        var patient_person_details = personLookUp.GetPersonById(PersonId);
                        var greencardlookup = new PersonGreenCardLookupManager();
                        var greencardptnpk = greencardlookup.GetPtnPkByPersonId(PersonId);

                        if (patient_person_details != null)
                        {
                            var maritalStatus =
                                new PersonMaritalStatusManager().GetCurrentPatientMaritalStatus(PersonId);
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
                            }
                            else if (LookupLogic.GetLookupNameById(patient_person_details.Sex) == "Female")
                            {
                                sex = 17;
                            }

                            foreach (var item in identifiersObjects)
                            {
                                if (item.Key == 1)
                                {
                                    enrollmentBlueCardId = item.Value;
                                }
                            }


                            if (greencardptnpk.Count == 0)
                            {
                                ptn_Pk = mstPatientLogic.InsertMstPatient(
                                    (patient_person_details.FirstName),
                                    (patient_person_details.LastName),
                                    (patient_person_details.MiddleName),
                                    facility.FacilityID, enrollmentBlueCardId, entryPointId,
                                    DateTime.Parse(enrollmentDate), sex,
                                    DateTime.Parse(personDateOfBirth),
                                    1, MaritalStatusId,
                                    address, phone, userId, Session["AppPosID"].ToString(),
                                    203, DateTime.Parse(enrollmentDate), DateTime.Now);

                                patient.ptn_pk = ptn_Pk;
                                patientManager.UpdatePatient(patient, patientId);
                            }
                            else
                            {
                                ptn_Pk = greencardptnpk[0].Ptn_Pk;
                                patient.ptn_pk = greencardptnpk[0].Ptn_Pk;
                                patientManager.UpdatePatient(patient, patientId);
                            }
                        }

                        Session["PatientMasterVisitId"] = patientMasterVisitId;

                        if (patientMasterVisitId > 0)
                        {
                            foreach (var item in identifiersObjects)
                            {
                                patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId,
                                patientEnrollmentId, item.Key, item.Value);
                                
                                var identifierManager = new IdentifierManager();
                                var identifierList = identifierManager.GetIdentifiersById(item.Key);
                                var hivtesting = _hivTestingManager.GetAll().OrderByDescending(y => y.Id).FirstOrDefault(n => n.PersonId == PersonId);
                                if (identifierList.Count > 0)
                                {
                                    if (identifierList[0].Code == "CCCNumber")
                                    {
                                        if (hivtesting != null)
                                        {
                                            hivtesting.ReferredToCare = true;
                                            _hivTestingManager.UpdatePatientHivTesting(hivtesting);

                                            PatientLinkage patLinkage = new PatientLinkage();
                                            patLinkage.LinkageDate = DateTime.Parse(enrollmentDate);
                                            patLinkage.CCCNumber = item.Value;
                                            patLinkage.PersonId = PersonId;
                                            patLinkage.CreatedBy = userId;
                                            patLinkage.Enrolled = true;
                                            patLinkage.PatientId = patientId;

                                            linkageManager.AddPatientLinkage(patLinkage);
                                        }
                                    }
                                }

                                
                            }
                                
                            if (greencardptnpk.Count == 0)
                            {
                                mstPatientLogic.AddOrdVisit(ptn_Pk, facilityId, DateTime.Now, 110,
                                    userId, DateTime.Now, 203);
                            }


                            message.errorcode = 0;
                            message.msg += "<p>Successfully enrolled patient.</p>";
                        }

                    }
                }
                else
                {
                    var patientLookManager = new PatientLookupManager();
                    List<PatientLookup> patient = patientLookManager.GetPatientByPersonId(PersonId);

                    if (patient.Count > 0)
                    {
                        Session["PatientPK"] = patient[0].Id;

                        List<PatientEntity> listPatient = new List<PatientEntity>();
                        var entity = patient.ConvertAll(x => new PatientEntity { Id = x.Id, Active = x.Active, DateOfBirth = x.DateOfBirth, ptn_pk = x.ptn_pk, PatientType = x.PatientType, PatientIndex = x.PatientIndex, NationalId = x.NationalId, FacilityId = x.FacilityId });
                        var patientAuditData = AuditDataUtility.Serializer(entity);

                        PatientEntity updatePatient = new PatientEntity();
                        updatePatient.ptn_pk = patient[0].ptn_pk;
                        updatePatient.DateOfBirth = patient[0].DateOfBirth;
                        updatePatient.NationalId = nationalId;
                        updatePatient.FacilityId = patient[0].FacilityId;


                        //listPatient.Add(entity);
                        updatePatient.AuditData = patientAuditData;
                        //var enrollmentAuditData = AuditDataUtility.Serializer(patient);

                        patientManager.UpdatePatient(updatePatient, patient[0].Id);

                        int patientMasterVisitId = patientMasterVisitManager.PatientMasterVisitCheckin(patient[0].Id, userId);
                        Session["PatientMasterVisitId"] = patientMasterVisitId;

                        List<PatientEntryPoint> entryPoints = patientEntryPointManager.GetPatientEntryPoints(patient[0].Id);

                        if (entryPoints.Count > 0)
                        {
                            var entryPointAuditData = AuditDataUtility.Serializer(entryPoints);

                            entryPoints[0].EntryPointId = entryPointId;
                            entryPoints[0].AuditData = entryPointAuditData;

                            patientEntryPointManager.UpdatePatientEntryPoint(entryPoints[0]);
                        }
                        foreach (var item in identifiersObjects)
                        {
                            var identifiersByPatientId = patientIdentifierManager
                                .GetPatientEntityIdentifiersByPatientId(patient[0].Id, item.Key);

                            if (identifiersByPatientId.Count > 0)
                            {
                                foreach (var entityIdentifier in identifiersByPatientId)
                                {
                                    int enrollmentId = entityIdentifier.PatientEnrollmentId;

                                    PatientEntityEnrollment entityEnrollment =
                                        patientEnrollmentManager.GetPatientEntityEnrollment(enrollmentId);
                                    List<PatientEntityEnrollment> listEnrollment = new List<PatientEntityEnrollment>();
                                    listEnrollment.Add(entityEnrollment);
                                    var enrollmentAuditData = AuditDataUtility.Serializer(listEnrollment);

                                    entityEnrollment.EnrollmentDate = DateTime.Parse(enrollmentDate);
                                    entityEnrollment.AuditData = enrollmentAuditData;

                                    patientEnrollmentManager.updatePatientEnrollment(entityEnrollment);

                                    var entityIdentifierAuditData = AuditDataUtility.Serializer(identifiersByPatientId);
                                    entityIdentifier.IdentifierValue = item.Value;
                                    entityIdentifier.AuditData = entityIdentifierAuditData;
                                    patientIdentifierManager.UpdatePatientIdentifier(entityIdentifier, facilityId);
                                }
                            }
                            else
                            {
                                patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patient[0].Id, enrollmentDate, userId);
                                patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patient[0].Id, entryPointId, userId);
                                patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patient[0].Id,
                                    patientEnrollmentId, item.Key, item.Value);
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
        public string AddReconfirmatoryTest(int reconfirmatoryTest , int resultReConfirmatoryTest, string reConfirmatoryTestDate)
        {
            ExMessage message = new ExMessage();
            try
            {
                PersonId = int.Parse(Session["PersonId"].ToString());
                int userId = Convert.ToInt32(Session["AppUserId"]);

                HivReConfirmatoryTestManager hivReConfirmatoryTestManager = new HivReConfirmatoryTestManager();
                int result = hivReConfirmatoryTestManager.AddHivReConfirmatoryTest(PersonId, reconfirmatoryTest, resultReConfirmatoryTest, reConfirmatoryTestDate, userId);

                if (result > 0)
                {
                    message.errorcode = 0;
                    message.msg += "<p>Successfully added patient re-confirmation test.</p>";
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
        public string UpdatePatientEnrollment(string enrollmentNo , string enrollmentDate, string serviceName)
        {
            ExMessage message = new ExMessage();

            try
            {
                var patientPK = Convert.ToInt32(Session["PatientPK"]);
                var patientEnrollment = new PatientEnrollmentManager();
                var patientIdentifier = new PatientIdentifierManager();
                var patientMasterVisitManager = new PatientMasterVisitManager();
                var patientManager = new PatientLookupManager();

                var lookupLogic = new LookupLogic();
                var patientIdentifiers = lookupLogic.GetItemIdByGroupAndDisplayName("PatientIdentifier", serviceName);
                var identifierId = 0;
                if (patientIdentifiers.Count > 0)
                {
                    identifierId = patientIdentifiers[0].ItemId;
                }

                var patient = patientManager.GetPatientDetailSummary(patientPK);
                if (DateTime.Parse(enrollmentDate) < DateTime.Parse(patient.DateOfBirth.ToString()))
                {
                    var DOBexception = new SoapException("Enrollment Date: " + enrollmentDate + " should not be before the date of birth " + DateTime.Parse(patient.DateOfBirth.ToString()).ToString("dd-MM-yyyy"), SoapException.ClientFaultCode);
                    throw DOBexception;
                }

                var enrollmentIdentifiers = lookupLogic.GetItemIdByGroupAndDisplayName("VisitType", "Enrollment");
                var itemId = 0;
                if (enrollmentIdentifiers.Count > 0)
                {
                    itemId = enrollmentIdentifiers[0].ItemId;
                }

                List<PatientMasterVisit> visitsNonEnrollments  = patientMasterVisitManager.GetNonEnrollmentVisits(patientPK, itemId);

                var identifierTypesCheck = patientIdentifier.CheckIfIdentifierNumberIsUsed(enrollmentNo, identifierId);

                if (identifierTypesCheck.Count > 0)
                {
                    if (patientPK != identifierTypesCheck[0].PatientId)
                    {
                        var exception = new SoapException("No: " + enrollmentNo + " already exists", SoapException.ClientFaultCode);
                        throw exception;
                    }               
                }

                if (visitsNonEnrollments.Count > 0)
                {
                    foreach (var items in visitsNonEnrollments)
                    {
                        if (DateTime.Parse(enrollmentDate) > items.VisitDate)
                        {
                            var newexception = new SoapException("Enrollment Date: " + enrollmentDate + " is after encounters in the system", SoapException.ClientFaultCode);
                            throw newexception;
                        }
                    }
                }

                List<PatientEntityEnrollment> entityEnrollments = new List<PatientEntityEnrollment>();

                if (patientPK > 0)
                {
                    entityEnrollments = patientEnrollment.GetPatientEnrollmentByPatientId(patientPK);

                    if (entityEnrollments.Count > 0)
                    {
                        var identifiers = patientIdentifier.GetPatientEntityIdentifiers(patientPK, entityEnrollments[0].Id, identifierId);

                        if (identifiers.Count > 0)
                        {
                            var identifiersAuditData = AuditDataUtility.Serializer(identifiers);
                            identifiers[0].IdentifierValue = enrollmentNo;
                            identifiers[0].AuditData = identifiersAuditData;


                            patientIdentifier.UpdatePatientIdentifier(identifiers[0], patient.FacilityId);
                        }

                        var enrollmentAuditData = AuditDataUtility.Serializer(entityEnrollments);

                        entityEnrollments[0].EnrollmentDate = DateTime.Parse(enrollmentDate);
                        entityEnrollments[0].AuditData = enrollmentAuditData;

                        patientEnrollment.updatePatientEnrollment(entityEnrollments[0]);

                        message.errorcode = 0;
                        message.msg += "<p>Successfully edited patient enrollment.</p>";
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

                patientId = int.Parse(Session["PatientPK"].ToString());
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
                    else if (String.IsNullOrWhiteSpace(facilityOutTransfer) && String.IsNullOrWhiteSpace(dateOfDeath))
                    {
                        careEndingManager.AddPatientCareEndingOther(patientId, patientMasterVisitId, patientEnrollmentId,
                            exitReason, DateTime.Parse(exitDate), GlobalObject.unescape(careEndingNotes));
                    }
                    else
                        careEndingManager.AddPatientCareEndingDeath(patientId, patientMasterVisitId, patientEnrollmentId,
                        exitReason, DateTime.Parse(exitDate), DateTime.Parse(dateOfDeath),  GlobalObject.unescape(careEndingNotes));

                    PatientEntityEnrollment entityEnrollment =
                        enrollmentManager.GetPatientEntityEnrollment(patientEnrollmentId);
                    entityEnrollment.CareEnded = true;
                    enrollmentManager.updatePatientEnrollment(entityEnrollment);
                    Session["EncounterStatusId"] = 0;
                    Session["PatientEditId"] = 0;
                    Session["PatientPK"] = 0;
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

            patientId = int.Parse(Session["PatientPK"].ToString());
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
        public string GetPatientEnrollmentDetails()
        {
            try
            {
                EnrollmentDetails enrollmentDetails = new EnrollmentDetails();
                enrollmentDetails.IndentifiersList = new List<Identifier>();
                PersonId = int.Parse(Session["PersonId"].ToString());
                PatientLookupManager patientLookupManager = new PatientLookupManager();
                PatientEntryPointManager entryPointManager = new PatientEntryPointManager();
                PatientIdentifierManager identifierManager = new PatientIdentifierManager();
                IdentifierManager ideManager = new IdentifierManager();
                PersonLookUpManager personLookUp = new PersonLookUpManager();

                List<PatientLookup> patientList = patientLookupManager.GetPatientByPersonId(PersonId);

                if (patientList.Count > 0)
                {
                    enrollmentDetails.DOB = String.Format("{0:dd-MMM-yyyy}", patientList[0].DateOfBirth);
                    enrollmentDetails.DobPrecision = patientList[0].DobPrecision;
                    if (enrollmentDetails.DOB == null)
                    {
                        PersonLookUp person = personLookUp.GetPersonById(PersonId);
                        if (person.DateOfBirth != null)
                        {
                            enrollmentDetails.DOB = String.Format("{0:dd-MMM-yyyy}", person.DateOfBirth);
                            enrollmentDetails.DobPrecision = person.DobPrecision==null?false:Convert.ToBoolean(person.DobPrecision);
                        }                   
                    }
                    enrollmentDetails.NationalId = patientList[0].NationalId;
                    enrollmentDetails.EnrollmentDate = String.Format("{0:dd-MMM-yyyy}", patientList[0].EnrollmentDate);

                    var entryPoints  = entryPointManager.GetPatientEntryPoints(patientList[0].Id);
                    var identifiers = identifierManager.GetAllPatientEntityIdentifiers(patientList[0].Id);
                    var dynamicFields = EnrollmentService.ServiceDynamicFields(1);
                    if (entryPoints.Count>0)
                    {
                        string Name = LookupLogic.GetLookupNameById(entryPoints[0].EntryPointId);
                        if (Name == "Unknown")
                        {
                            enrollmentDetails.EntryPointIdUnknown = true;
                            enrollmentDetails.EntryPointId = entryPoints[0].EntryPointId;
                        }
                        else
                        {
                            enrollmentDetails.EntryPointIdUnknown = false;
                            enrollmentDetails.EntryPointId = entryPoints[0].EntryPointId;
                        }                       
                    }
                    var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
                    foreach (var field in dynamicFields)
                    {
                        foreach (var itemsEntityIdentifier in identifiers)
                        {
                            if (itemsEntityIdentifier.IdentifierTypeId == field.ID)
                            {
                                var code = field.Code;
                                dynamicObject.Add(code, itemsEntityIdentifier.IdentifierValue);
                            }
                        }
                    }
                    //if (identifiers.Count > 0)
                    //{
                    //    enrollmentDetails.IndentifierId = identifiers[0].IdentifierTypeId;
                    //}

                    foreach (var dynamicItem in dynamicObject)
                    {
                        var key = dynamicItem.Key;
                        var identifier = ideManager.GetIdentifierByCode(key);
                        var dynIdentifier = new Identifier();
                        if (identifier.PrefixType != null)
                        {
                            string[] enrollmentParts = dynamicItem.Value.ToString().Split('-');
                            int parts = enrollmentParts.Length;
                            if (parts > 1)
                            {
                                dynIdentifier.Code = key;
                                dynIdentifier.PrefixType = enrollmentParts[0];
                                dynIdentifier.DataType = enrollmentParts[1];
                                //enrollmentDetails.EnrollmentValue = enrollmentParts[1];
                                //enrollmentDetails.Prefix = enrollmentParts[0];
                                //enrollmentDetails.Suffix = null;
                            }
                            else
                            {
                                dynIdentifier.Code = key;
                                dynIdentifier.DataType = dynamicItem.Value.ToString();
                                //enrollmentDetails.EnrollmentValue = dynamicItem.Value.ToString();
                            }
                        }
                        else
                        {
                            dynIdentifier.Code = key;
                            dynIdentifier.DataType = dynamicItem.Value.ToString();
                        }
                        enrollmentDetails.IndentifiersList.Add(dynIdentifier);
                    }
                    
                    //string[] enrollmentParts = patientList[0].EnrollmentNumber.Split('-');
                    //int parts = enrollmentParts.Length;
                    //if (parts > 1)
                    //{
                    //    enrollmentDetails.EnrollmentValue = enrollmentParts[1];
                    //    enrollmentDetails.Prefix = enrollmentParts[0];
                    //    enrollmentDetails.Suffix = null;
                    //}
                    //else
                    //{
                    //    enrollmentDetails.EnrollmentValue = patientList[0].EnrollmentNumber;
                    //}
                }
                else
                {
                    PersonLookUp person = personLookUp.GetPersonById(PersonId);
                    if (person.DateOfBirth != null)
                    {
                        enrollmentDetails.DOB = String.Format("{0:dd-MMM-yyyy}", person.DateOfBirth);
                        enrollmentDetails.DobPrecision = person.DobPrecision == null ? false : Convert.ToBoolean(person.DobPrecision);
                    }
                }

                return new JavaScriptSerializer().Serialize(enrollmentDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetFacilitiesList(string q)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;

                var facilityListManager = new FacilityListManager();
                var result = serializer.Serialize(facilityListManager.GetFacilitiesList(q));
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public string GetSelectedFacility(string mflcode)
        {
            var serializer = new JavaScriptSerializer();
            var facilityList = new FacilityListManager();

            return serializer.Serialize(facilityList.GetSelectedFacility(mflcode));
        }

        [WebMethod(EnableSession = true)]
        public string GetDynamicFields()
        {
            return new JavaScriptSerializer().Serialize(EnrollmentService.ServiceDynamicFields(1));
        }

        public static List<FormDetails> ServiceDynamicFields(int serviceId)
        {
            List<FormDetails> formDetails = new List<FormDetails>();
            try
            {
                var serviceareIdentifiersManager = new ServiceAreaIdentifiersManager();
                var identifierManager = new IdentifierManager();


                var identifiers = serviceareIdentifiersManager.GetIdentifiersByServiceArea(serviceId);
                if (identifiers.Count > 0)
                {
                    for (int i = 0; i < identifiers.Count; i++)
                    {
                        var resultIdentifiers = identifierManager.GetIdentifiersById(identifiers[i].IdentifierId);
                        if (resultIdentifiers.Count > 0)
                        {
                            for (int j = 0; j < resultIdentifiers.Count; j++)
                            {
                                var details = new FormDetails();

                                details.ID = resultIdentifiers[j].Id;
                                details.DataType = resultIdentifiers[j].DataType;
                                details.Label = resultIdentifiers[j].DisplayName;
                                details.Prefix = resultIdentifiers[j].PrefixType;
                                details.Required = identifiers[i].RequiredFlag;
                                details.Code = resultIdentifiers[j].Code;
                                details.IdentifierName = resultIdentifiers[j].Name;

                                formDetails.Add(details);
                            }
                        }
                    }
                }

                return formDetails;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
