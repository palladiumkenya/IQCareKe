using Application.Presentation;
using Entities.CCC.Appointment;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using Entities.CCC.Visit;
using Interface.CCC.Lookup;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Entities.CCC.Baseline;
using Entities.CCC.Consent;
using Entities.CCC.Screening;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using IQCare.CCC.UILogic.Screening;
using Microsoft.JScript;
using Convert = System.Convert;
using System.Web;
using System.Web.Script.Serialization;
using Entities.CCC.Encounter;
using Entities.CCC.Enrollment;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Visit;
using Entities.Common;

namespace IQCare.Web.CCC.WebService
{
    public class FamilyMembers
    {
        public string RelationshipPersonId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int sex { get; set; }
        public string dob { get; set; }
        public string dobPrecision { get; set; }
        public int relationshipId { get; set; }
        public string baselineHivStatusId { get; set; }
        public string baselineHivStatusDate { get; set; }
        public string hivTestingresultId { get; set; }
        public string hivTestingresultDate { get; set; }
        public bool cccreferal { get; set; }
        public string cccReferalNumber { get; set; }
        public DateTime? cccReferalDate { get; set; }
    }

    /// <summary>
    /// Summary description for PatientService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class PatientService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }
        string appointmentid;

        int? baselinehivid;

        [WebMethod(EnableSession = true)]
        public string AddpatientVitals(int patientId, int bpSystolic, int bpDiastolic, decimal heartRate, decimal height,
            decimal muac, int patientMasterVisitId, decimal respiratoryRate, decimal spo2, decimal tempreture,
            decimal weight, decimal bmi, decimal headCircumference,string bmiz,string weightForAge,string weightForHeight,DateTime visitDate,
            decimal ageforZ, string nursesComments)
        {
            try
            {
                PatientEncounterManager patientEncounterManager=new PatientEncounterManager();
                int facilityId = Convert.ToInt32(Session["AppPosID"]);
                int createdBy = Session["AppUserID"] != null ? Convert.ToInt32(Session["AppUserID"]) : 0;

                PatientVital patientVital = new PatientVital()
                {
                    PatientId = patientId,
                    BpSystolic = bpSystolic,
                    Bpdiastolic = bpDiastolic,
                    HeartRate = heartRate,
                    Height = height,
                    Muac = muac,
                    PatientMasterVisitId = patientMasterVisitId,
                    RespiratoryRate = respiratoryRate,
                    SpO2 = spo2,
                    Temperature = tempreture,
                    Weight = weight,
                    BMI = bmi,
                    HeadCircumference = headCircumference,
                    VisitDate = visitDate,
                    BMIZ = bmiz,
                    WeightForAge = weightForAge,
                    WeightForHeight = weightForHeight,
                    CreatedBy = createdBy
                };
                var vital = new PatientVitalsManager();
                Result = vital.AddPatientVitals(patientVital, facilityId);
                int userId = Convert.ToInt32(Session["AppUserId"]);

                if (Result > 0)
                {
                    Result = patientEncounterManager.AddpatientEncounter(patientId,patientMasterVisitId,patientEncounterManager.GetPatientEncounterId("EncounterType", "Triage-encounter".ToLower()),204, userId);
                    
                    if (Result > 0) { Msg = "Patient Vitals Added Successfully!"; }

                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientScreening(int patientId, int patientMasterVisitid,DateTime visitDate, int screeningTypeId, bool screeningDone, DateTime screeningDate, int screeningCategoryId, int screeningValueId, string comment, int userId)
        {
            try
            {
                var screening=new PatientScreeningManager();
                Result = screening.AddPatientScreening(patientId, patientMasterVisitid,visitDate,screeningTypeId,screeningDone, screeningDate,screeningCategoryId, screeningValueId,comment, userId);
                Msg = (Result > 0) ? "Patient Screening Added Successfully" : "";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientScreening(string screeningResponseString, int userId)
        {
            try
            {
                var screeningResponses = new JavaScriptSerializer().Deserialize<List<PatientScreening>>(screeningResponseString);
                var screening = new PatientScreeningManager();
                foreach (PatientScreening patientScreening in screeningResponses)
                {
                    patientScreening.Id = screening.CheckIfPatientScreeningExists((Int32)patientScreening.PatientId, (DateTime)patientScreening.VisitDate, (Int32)patientScreening.ScreeningCategoryId, (Int32)patientScreening.ScreeningTypeId);
                    if ( patientScreening.Id <= 0)
                    {
                        string comment="";
                        if(patientScreening.Comment=="null")
                        {
                            comment = "";
                        }
                        Result = screening.AddPatientScreening(patientScreening.PatientId, patientScreening.PatientMasterVisitId, (DateTime)patientScreening.VisitDate, (Int32)patientScreening.ScreeningTypeId, (bool)patientScreening.ScreeningDone, (DateTime)patientScreening.ScreeningDate, (Int32)patientScreening.ScreeningCategoryId, patientScreening.ScreeningValueId, comment, userId);
                    }
                    else {

                        string comment = "";
                        if (patientScreening.Comment == "null")
                        {
                            comment = "";
                        }
                        Result = screening.UpdatePatientScreening(patientScreening.Id, (DateTime)patientScreening.VisitDate,patientScreening.PatientId,patientScreening.PatientMasterVisitId, (Int32)patientScreening.ScreeningTypeId, (bool)patientScreening.ScreeningDone, (DateTime)patientScreening.ScreeningDate, (Int32)patientScreening.ScreeningCategoryId, patientScreening.ScreeningValueId, comment);
                    }
                }
                Msg = (Result > 0) ? "Patient Screening Updated Successfully" : "";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientScreening(int patientId, DateTime visitDate, int screeningcategoryId)
        {
            try
            {
                var screening = new PatientScreeningManager();
                
                var results = screening.GetPatientScreening(patientId, visitDate, screeningcategoryId);

                Msg = new JavaScriptSerializer().Serialize(results);

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPatientAppointment(int patientId, int patientMasterVisitId, DateTime appointmentDate, string description, int reasonId, int serviceAreaId, int statusId, int differentiatedCareId, int userId)
        {
            
           
            try
            {
                if (appointmentDate != DateTime.MinValue)
                {
                    PatientAppointment patientAppointment = new PatientAppointment()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        AppointmentDate = appointmentDate,
                        Description = description,
                        DifferentiatedCareId = differentiatedCareId,
                        ReasonId = reasonId,
                        ServiceAreaId = serviceAreaId,
                        StatusId = statusId,
                        CreatedBy = userId,
                        CreateDate = DateTime.Now
                    };

                    var appointment = new PatientAppointmentManager();
                    Result = appointment.AddPatientAppointments(patientAppointment);
                    if (Result > 0)
                    {
                        Msg = "Patient appointment Added Successfully!";
                    }
                }
                else
                {
                    Msg = "Patient appointment not Saved Successfully";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientFamilyTesting(string familyMembers)
        {
            string relationshipPersonId; int patientId; int patientMasterVisitId; string firstName; string middleName; string lastName; int sex; string dob; int relationshipId; string baselineHivStatusId; string baselineHivStatusDate;
            /*string hivTestingresultId;*/
            string hivTestingresultDate; bool cccreferal; string cccReferalNumber; int userId;
            DateTime? linkageDate;
            bool? dobPrecision = null;

            //FamilyMembers[] familyMembrs = JsonConvert.DeserializeObject<FamilyMembers[]>(familyMembers);
            FamilyMembers[] familyMembrs = new JavaScriptSerializer().Deserialize<FamilyMembers[]>(familyMembers);

            int count = familyMembrs.Length;
            for (int i = 0; i < count; i++)
            {
                patientId = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());
                userId = Convert.ToInt32(Session["AppUserId"]);
                relationshipPersonId = GlobalObject.unescape(familyMembrs[i].RelationshipPersonId);
                firstName = GlobalObject.unescape(familyMembrs[i].firstName);
                middleName = GlobalObject.unescape(familyMembrs[i].middleName);
                lastName = GlobalObject.unescape(familyMembrs[i].lastName);
                int hivresultId = familyMembrs[i].hivTestingresultId == "" ? 0 : Convert.ToInt32(familyMembrs[i].hivTestingresultId);
                sex = familyMembrs[i].sex;
                dob = familyMembrs[i].dob;
                DateTime? dateOfBirth = null;
                if (dob != "")
                {
                    dateOfBirth = DateTime.Parse(dob);
                }
                if (familyMembrs[i].dobPrecision != "")
                {
                    dobPrecision = Convert.ToBoolean(familyMembrs[i].dobPrecision);
                }
                if (familyMembrs[i].dobPrecision == "" || familyMembrs[i].dobPrecision == null)
                {
                    dobPrecision = false;
                }
                else
                {
                    dobPrecision = Convert.ToBoolean(familyMembrs[i].dobPrecision);
                }
                relationshipId = familyMembrs[i].relationshipId;



                baselineHivStatusId = familyMembrs[i].baselineHivStatusId;

                if (!(String.IsNullOrEmpty(baselineHivStatusId)))
                {
                    baselinehivid = Convert.ToInt32(baselineHivStatusId);

                }


                baselineHivStatusDate = familyMembrs[i].baselineHivStatusDate;
                cccreferal = familyMembrs[i].cccreferal;
                cccReferalNumber = familyMembrs[i].cccReferalNumber;
                hivTestingresultDate = familyMembrs[i].hivTestingresultDate;
                linkageDate = familyMembrs[i].cccReferalDate;

                PatientFamilyTesting patientFamilyTesting = new PatientFamilyTesting()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Sex = sex,
                    DateOfBirth = dateOfBirth,
                    DobPrecision = dobPrecision,
                    RelationshipId = relationshipId,
                    BaseLineHivStatusId = baselinehivid,
                    //BaselineHivStatusDate = baselineHivStatusDate,
                    //HivTestingResultsDate = hivTestingresultDate,
                    HivTestingResultsId = hivresultId,
                    CccReferal = cccreferal,
                    CccReferaalNumber = cccReferalNumber,
                    LinkageDate = linkageDate
                };

                if (hivTestingresultDate != "")
                    patientFamilyTesting.HivTestingResultsDate = DateTime.Parse(hivTestingresultDate);
                if (baselineHivStatusDate != "")
                    patientFamilyTesting.BaselineHivStatusDate = DateTime.Parse(baselineHivStatusDate);

                try
                {
                    var testing = new PatientFamilyTestingManager();
                    var fam =
                        testing.GetPatientFamilyList(patientId)
                            .Where(
                                x =>
                                    x.FirstName == firstName && x.MiddleName == middleName && x.LastName == lastName &&
                                    x.RelationshipId == relationshipId);
                    if (!fam.Any())
                    {

                        if (relationshipPersonId != null)
                        {
                            int relationshipPerson = Convert.ToInt32(relationshipPersonId.ToString());
                            if (Convert.ToInt32(relationshipPerson) > 0)
                            {
                                Result = testing.AddPatientFamilyTestingsExisting(patientFamilyTesting, userId, Convert.ToInt32(relationshipPerson));
                            }

                            else
                            {
                                Result = testing.AddPatientFamilyTestings(patientFamilyTesting, userId);
                            }
                        }
                        else
                        {
                            Result = testing.AddPatientFamilyTestings(patientFamilyTesting, userId);
                        }
                        if (Result > 0)
                        {
                            Msg = "Patient family testing Added Successfully!";
                        }

                    }
                        
                    
                    else
                    {
                        Msg = firstName + " " + middleName + " " + lastName + " Not saved. Family member already exists!";
                    }

                }
                catch (Exception e)
                {
                    Msg = e.Message;
                }
            }


            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientAsFamilyMember(int linkedPatientPersonId, int relationshipTypeId, string baselineDate, string cccNumber)
        {
            try
            {
                int patientId = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                int userId = Convert.ToInt32(Session["AppUserId"]);
                int patientMasterVisitId = Convert.ToInt32(Session["PatientMasterVisitId"].ToString());
                int baselineResult = 0; 

                LookupLogic logic = new LookupLogic();
                HivReConfirmatoryTestManager reConfirmatoryTestManager = new HivReConfirmatoryTestManager();

                
                var items = logic.GetItemIdByGroupAndItemName("BaseLineHivStatus", "Tested Positive");
                var positiveHivReconfirmation = logic.GetItemIdByGroupAndItemName("ReConfirmatoryTest", "Positive");
                if (items.Count > 0)
                {
                    baselineResult = items[0].ItemId;
                }

                if (positiveHivReconfirmation.Count > 0)
                {
                    int positiveHivReconfirmationId = positiveHivReconfirmation[0].ItemId;

                    HivReConfirmatoryTest hivReConfirmatoryTest = reConfirmatoryTestManager.GetPersonLastestReConfirmatoryTest(linkedPatientPersonId, positiveHivReconfirmationId);
                    if (hivReConfirmatoryTest != null)
                    {
                        baselineDate = hivReConfirmatoryTest.TestResultDate.ToString();
                    }
                }

                var testing = new PatientFamilyTestingManager();

                testing.AddLinkedPatientFamilyTesting(linkedPatientPersonId, patientId, patientMasterVisitId, baselineResult, DateTime.Parse(baselineDate), relationshipTypeId, userId, cccNumber);

                return "Successfully Linked";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public string UpdatePatientFamilyTesting(int patientId, int patientMasterVisitId, string firstName, string middleName, string lastName, int sex, string dob, int relationshipId, int baselineHivStatusId, DateTime baselineHivStatusDate, string hivTestingresultId, string hivTestingresultDate, bool cccreferal, string cccReferalNumber, int userId, int personRelationshipId, int hivTestingId, int personId,string cccReferalModDate)
        {
            firstName = GlobalObject.unescape(firstName);
            middleName = GlobalObject.unescape(middleName);
            lastName = GlobalObject.unescape(lastName);
            int hivresultId = hivTestingresultId == "" ? 0 : Convert.ToInt32(hivTestingresultId);
            //DateTime? testingResultsDate = hivTestingresultDate == "" ?  : DateTime.Parse(hivTestingresultDate);
            PatientFamilyTesting patientAppointment = new PatientFamilyTesting()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Sex = sex,
                DateOfBirth = DateTime.Parse(dob),
                RelationshipId = relationshipId,
                BaseLineHivStatusId = baselineHivStatusId,
                BaselineHivStatusDate = baselineHivStatusDate,
                //HivTestingResultsDate = testingResultsDate,
                HivTestingResultsId = hivresultId,
                CccReferal = cccreferal,
                CccReferaalNumber = cccReferalNumber,
                PersonRelationshipId = personRelationshipId,
                HivTestingId = hivTestingId,
                PersonId = personId
            };

            if(hivTestingresultDate != "")
                patientAppointment.HivTestingResultsDate = DateTime.Parse(hivTestingresultDate);
            if (cccReferalModDate != "")
            {
                patientAppointment.LinkageDate = DateTime.Parse(cccReferalModDate);
            }

            try
            {
                var testing = new PatientFamilyTestingManager();
                Result = testing.UpdatePatientFamilyTestings(patientAppointment, userId);
                if (Result > 0)
                {
                    Msg = "Patient family testing Updated Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPatientConsent(int patientId, int patientMasterVisitId, int consentType, DateTime consentDate)
        {
            // Todo properly save service area. Remove hack
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            int serviceArea = 0;
            List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            var sa = areas.FirstOrDefault();
            if (sa != null)
            {
                serviceArea = sa.ItemId;
            }

            PatientConsent patientConsent = new PatientConsent()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                ServiceAreaId = serviceArea,
                ConsentType = consentType,
                ConsentDate = consentDate
            };
            try
            {
                var consent = new PatientConsentManager();
                Result = consent.AddPatientConsents(patientConsent);
                if (Result > 0)
                {
                    Msg = "Patient consent added successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public IEnumerable<PatientAppointmentDisplay> GetPatientAppointments(string patientId)
        {
            List<PatientAppointmentDisplay> appointmentsDisplay = new List<PatientAppointmentDisplay>();
            IEnumerable<PatientAppointmentDisplay> listAppointments = new List<PatientAppointmentDisplay>();
            var appointments = new List<PatientAppointment>();
            var bluecardAppointments = new List<BlueCardAppointment>();
            try
            {
                var patientAppointment = new PatientAppointmentManager();
                int id = Convert.ToInt32(patientId);
                appointments = patientAppointment.GetByPatientId(id);
                bluecardAppointments = patientAppointment.GetBluecardAppointmentsByPatientId(id);
                foreach (var appointment in appointments)
                {
                    PatientAppointmentDisplay appointmentDisplay = Mapappointments(appointment);
                    appointmentsDisplay.Add(appointmentDisplay);
                }

                foreach (var appointment in bluecardAppointments)
                {
                    PatientAppointmentDisplay appointmentDisplay = MapBluecardappointments(appointment);
                    appointmentsDisplay.Add(appointmentDisplay);
                }

                listAppointments = appointmentsDisplay.OrderByDescending(n => n.AppointmentDate);

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return listAppointments;
        }

        [WebMethod]
        public PatientAppointmentDisplay GetExistingPatientAppointment(string patientId, DateTime appointmentDate, int serviceAreaId, int reasonId)
        {
            PatientAppointmentDisplay appointmentDisplay = new PatientAppointmentDisplay();
            PatientAppointment appointment = new PatientAppointment();
            try
            {
                var patientAppointment = new PatientAppointmentManager();
                int id = Convert.ToInt32(patientId);
                appointment = patientAppointment.GetByPatientId(id).FirstOrDefault(n => n.AppointmentDate.Date == appointmentDate.Date && n.ServiceAreaId == serviceAreaId && n.ReasonId == reasonId);
                if (appointment != null)
                {
                    appointmentDisplay = Mapappointments(appointment);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return appointmentDisplay;
        }
        [WebMethod(EnableSession =true)]
        public string GetPatientBaselineandHivTesting(int personId,int patientId)
        {
            string output=null;
            string CCCNumb="";
            try
            {

                PatientManager patientmanager = new PatientManager();


                int Currentpatient = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                int CurrentPersonId = patientmanager.GetPersonId(Currentpatient);

                if (CurrentPersonId == personId)
                {

                    string result = "self";
                    output = new JavaScriptSerializer().Serialize(result);
                }
                else
                {
                    PersonRelationshipManager rlm = new PersonRelationshipManager();
                    
                    PersonLookUpManager personlookup = new PersonLookUpManager();
                    PatientHivTestingManager pht = new PatientHivTestingManager();
                    PatientLinkageManager plm = new PatientLinkageManager();
                    List<PersonRelationship> relations = new List<PersonRelationship>();
                    List<PatientHivTesting> patientshivtest = new List<PatientHivTesting>();
                    List<PatientLinkage> patientslinkage = new List<PatientLinkage>();
                    PersonLookUp person = new PersonLookUp();
                    person = personlookup.GetPersonById(personId);
                    PatientLookupManager patientlookupman = new PatientLookupManager();
                    PatientLookup patientlook = new PatientLookup();
                    patientlook = patientlookupman.GetPatientByPersonId(person.Id);
                    if(patientlook !=null)
                    {
                        IdentifierManager im = new IdentifierManager();
                        Identifier id = new Identifier();
                        id=im.GetIdentifierByCode("CCCNumber");
                        PatientIdentifierManager pim = new PatientIdentifierManager();
                        PatientManager pm = new PatientManager();
                        if (patientlook.TransferIn == true)
                        {
                            PatientTransferInmanager pt = new PatientTransferInmanager();
                            List<PatientTransferIn> pti = pt.GetPatientTransferIns(patientlook.Id);
                            string mflcode = pti.Where(t => t != null && t.DeleteFlag == false).OrderByDescending(t => t.CreateDate).Select(t => t.MflCode).FirstOrDefault().ToString();

                            if (patientlook.EnrollmentNumber.Length < 5)
                            {
                                string CCCnum = string.Format("{0}-{1}", mflcode, patientlook.EnrollmentNumber);
                                CCCNumb = CCCnum;
                            }

                        }
                        else
                        {
                            if (patientlook.EnrollmentNumber.Length < 5)
                            {
                                string CCCnum = string.Format("{0}-{1}", patientlook.FacilityId, patientlook.EnrollmentNumber);
                                CCCNumb = CCCnum;

                            }
                            else
                            {
                                CCCNumb = patientlook.EnrollmentNumber;
                            }
                        }

                       // PatientEntity prl = new PatientEntity();
                        // prl = pm.GetPatientEntityByPersonId(personId);

                       // List<PatientEntityIdentifier> pid = new List<PatientEntityIdentifier>();

                            // pid=pim.GetPatientEntityIdentifiersByPatientId(prl.Id, id.Id);
                         // string CCCNumber = pid.Where(t => t != null && t.PatientId == prl.Id && t.DeleteFlag == false).OrderByDescending(t => t.CreateDate).Select(t=>t.IdentifierValue).FirstOrDefault();

                       /// CCCNumb = CCCNumber;
                    }
                    PersonRelationship rl = new PersonRelationship();
                    rl = rlm.GetSpecificRelationship(Currentpatient, personId);
                    relations.Add(rl);
                    PatientHivTesting ph = new PatientHivTesting();
                    ph = pht.GetPatientHivTesting(personId);
                    PatientLinkage patientl = new PatientLinkage();
                    patientl = plm.GetPatientLinkage(personId);
                    patientshivtest.Add(ph);
                    patientslinkage.Add(patientl);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (CCCNumb != null)
                    {
                        dic.Add("CCCNumber", CCCNumb);
                    }
                    else
                    {
                        dic.Add("CCCNumber", "0");
                    }
                    if (patientlook != null) { 
                        dic.Add("EnrollmentNumber", patientlook.EnrollmentNumber);
                     }
                     else
                        {
                         dic.Add("EnrollmentNumber", "0");
                     }
                    dic.Add("PersonId", person.Id.ToString());
                    dic.Add("FirstName", person.FirstName);
                    dic.Add("MiddleName", person.MiddleName);
                    dic.Add("DOB", patientlook.DateOfBirth.ToString());
                    dic.Add("LastName", person.LastName);
                    dic.Add("gender", person.Sex.ToString());
                    dic.Add("DobPrecision", patientlook.DobPrecision.ToString());
                    dic.Add("Relationshiptype", relations.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.RelationshipTypeId.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("BaselineResult", relations.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.BaselineResult.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("BaselineDate", relations.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.BaselineDate.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("HivTestingResult", patientshivtest.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.TestingResult.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("HivTestingDate", patientshivtest.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.TestingDate.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("ReferredToCare", patientshivtest.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.ReferredToCare.ToString()).DefaultIfEmpty(null).FirstOrDefault());
                    dic.Add("LinkageDate", patientslinkage.Where(t => t != null && t.PersonId == Convert.ToInt32(personId)).Select(t => t.LinkageDate.ToString()).DefaultIfEmpty(null).FirstOrDefault());

                    var data = dic.ToArray();

                    output = new JavaScriptSerializer().Serialize(data);
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
        

        [WebMethod(EnableSession = true)]
        public string GetPatientSearchList(List<Data> dataPayLoad)
        {
            String output = null;
            int filteredRecords = 0;
            int totalCount = 0;

           int  Currentpatient = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
            PersonRelationshipManager rlm = new PersonRelationshipManager();

            PatientHivTestingManager pht = new PatientHivTestingManager();
            PatientLinkageManager plm = new PatientLinkageManager();

            var jsonData = new List<PatientLookup>();

            try
            {
                string patientId = "";
                string firstName = null;
                string middleName = null;
                string lastName = null;
                string isEnrolled = null;

                PatientLookupManager patientLookup = new PatientLookupManager();


                patientId = dataPayLoad.FirstOrDefault(x => x.name == "patientId").value;
                firstName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "firstName").value);
                middleName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "middleName").value);
                lastName = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "lastName").value);
                isEnrolled = Convert.ToString(dataPayLoad.FirstOrDefault(x => x.name == "isEnrolled").value);

                //patientId = patientId != "" ? patientId : 0;

                if (patientId != "" || !string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(middleName) || !string.IsNullOrWhiteSpace(lastName))
                {
                    jsonData = patientLookup.GetPatientSearchListPayload(patientId, isEnrolled, firstName, middleName, lastName);
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

                      
                        data = jsonData.Select(x => new string[]
                        {

//(isEnrolled=="notEnrolledClients")? x.PersonId.ToString(): x.Id.ToString(),
                            (isEnrolled=="notEnrolledClients")? "0": x.Id.ToString(),
                            x.EnrollmentNumber.ToString(),
                            x.FirstName,
                            x.MiddleName,
                            x.LastName,
                            x.DateOfBirth.ToString("dd-MMM-yyyy"),
                            LookupLogic.GetLookupNameById(x.Sex),
                           
                    //x.RegistrationDate.ToString("dd-MMM-yyyy"),
                    (isEnrolled=="notEnrolledClients")? Convert.ToDateTime(x.RegistrationDate).ToString("dd-MMM-yyyy") : x.EnrollmentDate.ToString("dd-MMM-yyyy"),
                            x.PatientStatus.ToString(),
                              x.PersonId.ToString()
                             //relations.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.RelationshipTypeId.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             //relations.Where(t=>t!=null && t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.BaselineResult.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             //relations.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.BaselineDate.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             //patientshivtest.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.TestingResult.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             //patientshivtest.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.TestingDate.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             // patientshivtest.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.ReferredToCare.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                             //patientslinkage.Where(t=>t!=null &&t.PersonId==Convert.ToInt32(x.PersonId)).Select(t=>t.LinkageDate.ToString()).DefaultIfEmpty("null").FirstOrDefault(),
                          
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
        public List<PatientFamilyDisplay> GetFamilyTestings(string patientId)
        {
            List<PatientFamilyDisplay> familyDisplays = new List<PatientFamilyDisplay>();
            List<PatientFamilyTesting> familytestings = new List<PatientFamilyTesting>();
            try
            {
                var patientFamily = new PatientFamilyTestingManager();
                int id = Convert.ToInt32(patientId);
                familytestings = patientFamily.GetPatientFamilyList(id);
                foreach (var member in familytestings)
                {
                    PatientFamilyDisplay familyDisplay = MapMembers(member);
                    familyDisplays.Add(familyDisplay);
                }

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return familyDisplays;
        }

        [WebMethod]
        public int GetPatientAppointmentCount(DateTime date)
        {
            int count = 0;
            List<PatientAppointment> appointments = new List<PatientAppointment>();
            try
            {
                var appointment = new PatientAppointmentManager();
                appointments = appointment.GetByDate(date);
                count = appointments.Count;
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return count;
        }
        [WebMethod(EnableSession = true)]
        public string getAppointmentId(int PatientMasterVisitId, DateTime date)
        {
           var appointmentManager = new PatientAppointmentManager();
           string PatientId = Session["PatientPk"].ToString();
            PatientAppointment[] pAppointment = appointmentManager.GetAppointmentId(Convert.ToInt32(Session["PatientPK"]),PatientMasterVisitId, date).ToArray();
            //PatientClinicalNotes[] patientNotesData = PCN.getPatientClinicalNotesByVisitId(PatientId, PatientMasterVisitId).ToArray();
            string jsonNotesObject = "[]";
            jsonNotesObject = new JavaScriptSerializer().Serialize(pAppointment);
            return jsonNotesObject;
        }
        [WebMethod]
        public List<PatientConsentDisplay> GetpatientConsent(string patientId)
        {
            List<PatientConsentDisplay> consentDisplays = new List<PatientConsentDisplay>();
            List<PatientConsent> consents = new List<PatientConsent>();
            try
            {
                var patientConsent = new PatientConsentManager();
                int id = Convert.ToInt32(patientId);
                consents = patientConsent.GetByPatientId(id);
                foreach (var consent in consents)
                {
                    PatientConsentDisplay consentDisplay = MapConsent(consent);
                    consentDisplays.Add(consentDisplay);
                }

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return consentDisplays;
        }

        [WebMethod]
        public string AddPatientCategorization(int patientId, int patientMasterVisitId, string artRegimenPeriod, string activeOis, string visitsAdherant, string vlCopies, string ipt, string bmi, string age, string healthcareConcerns)
        {
            PatientCategorizationStatus categorizationStatus;
            string[] arr1 = new string[]{};

            if (Convert.ToBoolean(activeOis) && Convert.ToBoolean(artRegimenPeriod) && Convert.ToBoolean(visitsAdherant) && Convert.ToBoolean(vlCopies) && Convert.ToBoolean(ipt) && Convert.ToBoolean(age) && Convert.ToBoolean(healthcareConcerns) && Convert.ToBoolean(bmi))
                categorizationStatus = PatientCategorizationStatus.Stable;
            else
                categorizationStatus = PatientCategorizationStatus.UnStable;

            var patientCategorization = new PatientCategorization()
            {
                PatientId = patientId,
                Categorization = categorizationStatus,
                DateAssessed = DateTime.Now,
                PatientMasterVisitId = patientMasterVisitId
            };
            try
            {
                var categorization = new PatientCategorizationManager();
                Result = categorization.AddPatientCategorization(patientCategorization);
                if (Result > 0)
                {
                    Msg = "Patient Categorization Added Successfully!";

                    var lookUpLogic = new LookupLogic();
                    var status = lookUpLogic.GetItemIdByGroupAndItemName("StabilityAssessment", categorizationStatus.ToString());
                    var itemId = 0;
                    if (status.Count > 0)
                    {
                        itemId = status[0].ItemId;
                    }

                    arr1 = new string[] { Msg, itemId.ToString() };

                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }

            return new JavaScriptSerializer().Serialize(arr1);
        }

        [WebMethod]
        public bool CccNumberExists(string cccNumber)
        {
            PatientLinkageManager linkageManager = new PatientLinkageManager();
            return linkageManager.CccNumberExists(cccNumber);
        }
        private PatientFamilyDisplay MapMembers(PatientFamilyTesting member)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            string relationship = "";
            string baselineHivStatus = "";
            string hivStatus = "";
            string sex = "";
            List<LookupItemView> relationships = mgr.GetLookItemByGroup("Relationship");
            var s = relationships.FirstOrDefault(n => n.ItemId == member.RelationshipId);
            if (s != null)
            {
                relationship = s.ItemDisplayName;
            }
            List<LookupItemView> reasons = mgr.GetLookItemByGroup("BaseLineHivStatus");
            var r = reasons.FirstOrDefault(n => n.ItemId == member.BaseLineHivStatusId);
            if (r != null)
            {
                baselineHivStatus = r.ItemDisplayName;
            }
            List<LookupItemView> areas = mgr.GetLookItemByGroup("HivTestingResult");
            var sa = areas.FirstOrDefault(n => n.ItemId == member.HivTestingResultsId);
            if (sa != null)
            {
                hivStatus = sa.ItemDisplayName;
            }
            List<LookupItemView> genders = mgr.GetLookItemByGroup("Gender");
            var x = genders.FirstOrDefault(n => n.ItemId == member.Sex);
            if (x != null)
            {
                sex = x.ItemDisplayName;
            }

            PatientFamilyDisplay familyMemberDisplay = new PatientFamilyDisplay()
            {
                FirstName = member.FirstName,
                MiddleName = member.MiddleName,
                LastName = member.LastName,
                Sex = sex,
                DateOfBirth = member.DateOfBirth,
                DobPrecision = member.DobPrecision,
                Relationship = relationship,
                BaseLineHivStatus = baselineHivStatus,
                BaseLineHivStatusDate = member.BaselineHivStatusDate,
                HivStatusResult = hivStatus,
                HivStatusResultDate = member.HivTestingResultsDate,
                CccReferal = member.CccReferal.ToString(),
                CccReferalNumber = member.CccReferaalNumber,
                PersonRelationshipId = member.PersonRelationshipId,
                HivTestingId = member.HivTestingId,
                PersonId = member.PersonId,
                LinkageDate = member.LinkageDate
            };
            return familyMemberDisplay;
        }

        private PatientConsentDisplay MapConsent(PatientConsent pc)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            string consentType = "";
            List<LookupItemView> type = mgr.GetLookItemByGroup("ConsentType");
            var s = type.FirstOrDefault(n => n.ItemId == pc.ConsentType);
            if (s != null)
            {
                consentType = s.ItemDisplayName;
            }

            PatientConsentDisplay patientConsentDisplay = new PatientConsentDisplay()
            {
                ConsentDate = pc.ConsentDate,
                ConsentType = consentType
            };

            return patientConsentDisplay;
        }
        private PatientAppointmentDisplay Mapappointments(PatientAppointment a)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            appointmentid = a.Id.ToString();
            string status = "";
            string reason = "";
            string serviceArea = "";
            string differentiatedCare = "";
            string editAppointment = "<a href='ScheduleAppointment.aspx?appointmentid="+appointmentid+"' type='button' class='btn btn-success fa fa-pencil-square btn-fill' > Edit</a>";
            string deleteAppointment = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
            List <LookupItemView> statuses = mgr.GetLookItemByGroup("AppointmentStatus");
            var s = statuses.FirstOrDefault(n => n.ItemId == a.StatusId);
            if (s != null)
            {
                status = s.ItemDisplayName;
            }
            List<LookupItemView> reasons = mgr.GetLookItemByGroup("AppointmentReason");
            var r = reasons.FirstOrDefault(n => n.ItemId == a.ReasonId);
            if (r != null)
            {
                reason = r.ItemDisplayName;
            }
            List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            var sa = areas.FirstOrDefault(n => n.ItemId == a.ServiceAreaId);
            if (sa != null)
            {
                serviceArea = sa.ItemDisplayName;
            }
            List<LookupItemView> care = mgr.GetLookItemByGroup("DifferentiatedCare");
            var dc = care.FirstOrDefault(n => n.ItemId == a.DifferentiatedCareId);
            if (dc != null)
            {
                differentiatedCare = dc.ItemDisplayName;
            }
            PatientAppointmentDisplay appointment = new PatientAppointmentDisplay()
            {
                ServiceArea = serviceArea,
                Reason = reason,
                AppointmentDate = a.AppointmentDate,
                Description = a.Description,
                Status = status,
                DifferentiatedCare = differentiatedCare,
                EditAppointment = editAppointment,
                DeleteAppointment = deleteAppointment,
                AppointmentId = appointmentid
            };

            return appointment;

            //ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //string status = "";
            //string reason = "";
            //string serviceArea = "";
            //string differentiatedCare = "";
            //List <LookupItemView> statuses = mgr.GetLookItemByGroup("AppointmentStatus");
            //var s = statuses.FirstOrDefault(n => n.ItemId == a.StatusId);
            //if (s != null)
            //{
            //    status = s.ItemDisplayName;
            //}
            //List<LookupItemView> reasons = mgr.GetLookItemByGroup("AppointmentReason");
            //var r = reasons.FirstOrDefault(n => n.ItemId == a.ReasonId);
            //if (r != null)
            //{
            //    reason = r.ItemDisplayName;
            //}
            //List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            //var sa = areas.FirstOrDefault(n => n.ItemId == a.ServiceAreaId);
            //if (sa != null)
            //{
            //    serviceArea = sa.ItemDisplayName;
            //}
            //List<LookupItemView> care = mgr.GetLookItemByGroup("DifferentiatedCare");
            //var dc = care.FirstOrDefault(n => n.ItemId == a.DifferentiatedCareId);
            //if (dc != null)
            //{
            //    differentiatedCare = dc.ItemDisplayName;
            //}
            //PatientAppointmentDisplay appointment = new PatientAppointmentDisplay()
            //{
            //    ServiceArea = serviceArea,
            //    Reason = reason,
            //    AppointmentDate = a.AppointmentDate,
            //    Description = a.Description,
            //    Status = status,
            //    DifferentiatedCare = differentiatedCare
            //};

            //return appointment;
        }

        [WebMethod]
        public string DeleteAppointment(int AppointmentId)
        {
            try
            {
                var appointment = new PatientAppointmentManager();
                appointment.DeletePatientAppointments(AppointmentId);
                Msg = "Appointment Deleted Successfully!";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string UpdatePatientAppointment(int patientId, int patientMasterVisitId, DateTime appointmentDate, string description, int reasonId, int serviceAreaId, int statusId, int differentiatedCareId, int userId, int appointmentId)
        {

            PatientAppointment patientAppointment = new PatientAppointment()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AppointmentDate = appointmentDate,
                Description = description,
                DifferentiatedCareId = differentiatedCareId,
                ReasonId = reasonId,
                ServiceAreaId = serviceAreaId,
                StatusId = statusId,
                CreatedBy = userId,
                CreateDate = DateTime.Now,
                Id = appointmentId
            };
            try
            {
                var appointment = new PatientAppointmentManager();
                Result = appointment.UpdatePatientAppointments(patientAppointment);
                if (Result > 0)
                {
                    Msg = "Patient appointment Updated Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        private PatientAppointmentDisplay MapBluecardappointments(BlueCardAppointment bluecardAppointment)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            string appointmentDescription = String.Empty;
            string serviceArea = String.Empty;
            string appointmentReason = String.Empty;

            if (bluecardAppointment.Description != null)
            {
                appointmentDescription = bluecardAppointment.Description;
            }

            if (bluecardAppointment.ServiceArea != null)
            {
                serviceArea = bluecardAppointment.ServiceArea;
            }

            if (bluecardAppointment.Reason != null)
            {
                appointmentReason = bluecardAppointment.Reason;
            }

            PatientAppointmentDisplay appointment = new PatientAppointmentDisplay()
            {
                ServiceArea = serviceArea,
                Reason = appointmentReason,
                AppointmentDate = bluecardAppointment.AppointmentDate,
                Description = appointmentDescription,
                Status = bluecardAppointment.AppointmentStatus,
                DifferentiatedCare = " "
            };

            return appointment;
        }
    }



    public class PatientAppointmentDisplay
    {
        public string AppointmentId { get; set; }
        public string ServiceArea { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string DifferentiatedCare { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EditAppointment { get; set; }
        public string DeleteAppointment { get; set; }
    }

    public class PatientFamilyDisplay
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? DobPrecision { get; set; }
        public string Sex { get; set; }
        public string BaseLineHivStatus { get; set; }
        public DateTime ? BaseLineHivStatusDate { get; set; }
        public string HivStatusResult { get; set; }
        public DateTime ? HivStatusResultDate { get; set; }
        public string CccReferal { get; set; }
        public string CccReferalNumber { get; set; }
        public int PersonRelationshipId { get; set; }
        public int HivTestingId { get; set; }
        public int PersonId { get; set; }
        public DateTime? LinkageDate { get; set; }
    }

    public class PatientConsentDisplay
    {
        public string ConsentType { get; set; }
        public DateTime ConsentDate { get; set; }
    }
}