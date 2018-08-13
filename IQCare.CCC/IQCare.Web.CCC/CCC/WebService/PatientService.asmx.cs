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

namespace IQCare.Web.CCC.WebService
{
    public class FamilyMembers
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int sex { get; set; }
        public string dob { get; set; }
        public string dobPrecision { get; set; }
        public int relationshipId { get; set; }
        public int baselineHivStatusId { get; set; }
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
                        Result = screening.AddPatientScreening(patientScreening.PatientId, patientScreening.PatientMasterVisitId, (DateTime)patientScreening.VisitDate, (Int32)patientScreening.ScreeningTypeId, (bool)patientScreening.ScreeningDone, (DateTime)patientScreening.ScreeningDate, (Int32)patientScreening.ScreeningCategoryId, patientScreening.ScreeningValueId, patientScreening.Comment, userId);
                    }
                    else {
                        Result = screening.UpdatePatientScreening(patientScreening.PatientId, (DateTime)patientScreening.VisitDate, (Int32)patientScreening.ScreeningTypeId, (bool)patientScreening.ScreeningDone, (DateTime)patientScreening.ScreeningDate, (Int32)patientScreening.ScreeningCategoryId, patientScreening.ScreeningValueId, patientScreening.Comment);
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
            try
            {
                var appointment = new PatientAppointmentManager();
                Result = appointment.AddPatientAppointments(patientAppointment);
                if (Result > 0)
                {
                    Msg = "Patient appointment Added Successfully!";
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
            int patientId; int patientMasterVisitId; string firstName; string middleName; string lastName; int sex; string dob; int relationshipId; int baselineHivStatusId; string baselineHivStatusDate; /*string hivTestingresultId;*/ string hivTestingresultDate; bool cccreferal; string cccReferalNumber;  int userId;
            DateTime? linkageDate;
            bool dobPrecision;

            //FamilyMembers[] familyMembrs = JsonConvert.DeserializeObject<FamilyMembers[]>(familyMembers);
            FamilyMembers[] familyMembrs = new JavaScriptSerializer().Deserialize<FamilyMembers[]>(familyMembers);

            int count = familyMembrs.Length;
            for(int i=0; i < count; i++)
            {
                patientId = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());
                userId = Convert.ToInt32(Session["AppUserId"]);

                firstName = GlobalObject.unescape(familyMembrs[i].firstName);
                middleName = GlobalObject.unescape(familyMembrs[i].middleName);
                lastName = GlobalObject.unescape(familyMembrs[i].lastName);
                int hivresultId = familyMembrs[i].hivTestingresultId == "" ? 0 : Convert.ToInt32(familyMembrs[i].hivTestingresultId);
                sex = familyMembrs[i].sex;
                dob = familyMembrs[i].dob;
                dobPrecision = Convert.ToBoolean(familyMembrs[i].dobPrecision);
                relationshipId = familyMembrs[i].relationshipId;
                baselineHivStatusId = familyMembrs[i].baselineHivStatusId;
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
                    DateOfBirth = DateTime.Parse(dob),
                    DobPrecision = dobPrecision,
                    RelationshipId = relationshipId,
                    BaseLineHivStatusId = baselineHivStatusId,
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
                        Result = testing.AddPatientFamilyTestings(patientFamilyTesting, userId);
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
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
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