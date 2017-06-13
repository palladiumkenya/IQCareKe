using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using Entities.CCC.Enrollment;
using Entities.Common;
using Entities.PatientCore;
using IQCare.CCC.UILogic;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;
using Application.Common;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Enrollment;
using Microsoft.JScript;
using Convert = System.Convert;

namespace IQCare.Web.CCC.WebService
{
    public class PatientDetails
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string GenderString { get; set; }
        public string PersonDoB { get; set; }
        public string EnrollmentNumber { get; set; }
        public string EnrollmentDate { get; set; }
        public int ChildOrphan { get; set; }
        public string ChildOrphanString { get; set; }
        public int Inschool { get; set; }
        public string InschoolString { get; set; }
        public string NationalId { get; set; }
        public int MaritalStatusId { get; set; }
        public string MaritalStatusString { get; set; }
        public string GurdianFNames { get; set; }
        public string GurdianMName { get; set; }
        public string GurdianLName { get; set; }
        public int GuardianGender { get; set; }
        public string Age { get; set; }
        public int CountyId { get; set; }
        public string NearestHealthCentre { get; internal set; }
        public string LandMark { get; internal set; }
        public string SubLocation { get; internal set; }
        public string Location { get; internal set; }
        public string Village { get; internal set; }
        public int? Ward { get; internal set; }
        public int? SubCounty { get; internal set; }
        public string PatientPostalAddress { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string AlternativeNumber { get; internal set; }
        public string MobileNumber { get; internal set; }
        public string tsFname { get; internal set; }
        public string ISContacts { get; internal set; }
        public int tsGender { get; internal set; }
        public string tsLastName { get; internal set; }
        public string tsMiddleName { get; internal set; }
        public string population { get; internal set; }
        public int populationTypeId { get; internal set; }
        public int PopulationCategoryId { get; internal set; }
        public string PopulationCategoryString { get; set; }
        public int GuardianId { get; set; }
        public int PatientTreatmentSupporterId { get; set; }
        public int PatientType { get; set; }
        public string PatientTypeString { get; set; }
        public string EntryPoint { get; set; }

        public string GetAge(DateTime DateOfBirth)
        {
            TimeSpan age = DateTime.Now - DateOfBirth;
            int Year = DateTime.Now.Year - DateOfBirth.Year;
            if (DateOfBirth.AddYears(Year) > DateTime.Now) Year--;

            return Year.ToString();
        }
    }
    /// <summary>
    /// Summary description for PersonSeervice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PersonService : System.Web.Services.WebService
    {
        
        private int PersonId { get; set; }
        private int PersonGuardianId { get; set; }
        private int PersonTreatmentSupporterId { get; set; }
        private string Msg { get; set; }
        private int Result { get; set; }

        private List<PatientLookup> Patient { get; set; }
       // Utility _utility = new Utility();
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        [WebMethod(EnableSession = true)]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, int maritalStatusId, int userId, string dob, string nationalId, string patientid, string patientType)
        {
            patientid = patientid == "null" ? null : patientid;
            patientid = patientid == "" ? null : patientid;

            firstname = GlobalObject.unescape(firstname);
            middlename = GlobalObject.unescape(middlename);
            lastname = GlobalObject.unescape(lastname);
            nationalId = GlobalObject.unescape(nationalId);

            try
            {
                int perId = 0;
                int personId = 0;

                if (Session["PersonId"] != null)
                {
                    perId = int.Parse(Session["PersonId"].ToString());
                }
                
                if ((perId >0) || (patientid !=null && int.Parse(patientid) > 0))
                {
                    var personManager = new PersonManager();
                    var patientLogic = new PatientLookupManager();
                    if (perId > 0)
                    {
                        personId = perId;
                    }
                    else
                    {
                        var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                        personId = patient.PersonId;
                    }

                    personManager.UpdatePerson(firstname, middlename, lastname, gender, userId , personId);
                    Session["PersonId"] = personId;

                    Msg = "<p>Person Updated successfully</p>";

                    //PersonId = Convert.ToInt32(Session["PersonId"]);

                    var maritalStatus = new PersonMaritalStatusManager();
                    var _matStatus = maritalStatus.GetInitialPatientMaritalStatus(personId);

                    //int matStatusId = 0;
                    //var lookUpLogic = new LookupLogic();
                    //matStatusId = maritalStatusId > 0 ? maritalStatusId : lookUpLogic.GetItemIdByGroupAndItemName("MaritalStatus", "Single")[0].ItemId;

                    if (_matStatus != null && maritalStatusId > 0)
                    {
                        _matStatus.MaritalStatusId = maritalStatusId;
                        _matStatus.CreatedBy = userId;

                        Result = maritalStatus.UpdatePatientMaritalStatus(_matStatus);
                        if (Result > 0)
                        {
                            Msg += "<p>Person Marital Status Updated Successfully!</p>";
                            Session["PersonDob"] = DateTime.Parse(dob);
                            Session["NationalId"] = nationalId;
                            Session["PatientType"] = patientType;
                            var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                            if (patType == "Transit")
                            {
                                Session["NationalId"] = 99999999;
                            }
                        }

                    }
                    else if (_matStatus != null && maritalStatusId == 0)
                    {
                        _matStatus.DeleteFlag = true;

                        Result = maritalStatus.UpdatePatientMaritalStatus(_matStatus);
                        if (Result > 0)
                        {
                            Msg += "<p>Person Marital Status Updated Successfully!</p>";
                            Session["PersonDob"] = DateTime.Parse(dob);
                            Session["NationalId"] = nationalId;
                            Session["PatientType"] = patientType;
                            var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                            if (patType == "Transit")
                            {
                                Session["NationalId"] = 99999999;
                            }
                        }
                    }
                    else
                    {
                        if (maritalStatusId == 0)
                        {
                            Session["PersonDob"] = DateTime.Parse(dob);
                            Session["NationalId"] = nationalId;
                            Session["PatientType"] = patientType;
                            var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                            if (patType == "Transit")
                            {
                                Session["NationalId"] = 99999999;
                            }
                        }
                        else
                        {
                            Result = maritalStatus.AddPatientMaritalStatus(personId, maritalStatusId, userId);
                            if (Result > 0)
                            {
                                Msg += "<p>Person Marital Status Added Successfully!</p>";
                                Session["PersonDob"] = DateTime.Parse(dob);
                                Session["NationalId"] = nationalId;
                                Session["PatientType"] = patientType;
                                var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                                if (patType == "Transit")
                                {
                                    Session["NationalId"] = 99999999;
                                }
                            }
                        }
                    }                      
                }
                else
                {
                    var personLogic = new PersonManager();

                    PersonId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, userId);
                    Session["PersonId"] = PersonId;
                    if (PersonId > 0)
                    {
                        Msg = "<p>New Person Added successfully : PersonId=> " + PersonId + " </p>";

                        PersonId = Convert.ToInt32(Session["PersonId"]);
                        var maritalStatus = new PersonMaritalStatusManager();
                        var lookUpLogic = new LookupLogic();

                        if (maritalStatusId > 0)
                        {
                            Result = maritalStatus.AddPatientMaritalStatus(PersonId, maritalStatusId, userId);
                            if (Result > 0)
                            {
                                Msg += "<p>Person Marital Status Added Successfully!</p>";
                                Session["PersonDob"] = DateTime.Parse(dob);
                                Session["NationalId"] = nationalId;
                                Session["PatientType"] = patientType;
                                var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                                if (patType == "Transit")
                                {
                                    Session["NationalId"] = 99999999;
                                }
                            }
                        }
                        else
                        {
                            Session["PersonDob"] = DateTime.Parse(dob);
                            Session["NationalId"] = nationalId;
                            Session["PatientType"] = patientType;
                            var patType = LookupLogic.GetLookupNameById(int.Parse(patientType));
                            if (patType == "Transit")
                            {
                                Session["NationalId"] = 99999999;
                            }
                        }
                    }
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }
            
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonGuardian(string firstname, string middlename, string lastname, int gender, string orphan, string inSchool, int userId, string patientid)
        {
            patientid = patientid == "null" ? null : patientid;
            patientid = patientid == "" ? null : patientid;

            bool _orphan;
            bool _inSchool;

            _orphan = orphan == "Yes" ? true : false;
            _inSchool = inSchool == "Yes" ? true : false;

            try
            {
                int guardId = 0;
                if (Session["PersonGuardianId"] != null)
                {
                    guardId = int.Parse(Session["PersonGuardianId"].ToString());
                }

                if ((guardId>0) || (patientid !=null && int.Parse(patientid) > 0))
                {
                    var personLogic = new PersonManager();
                    var patientLogic = new PatientLookupManager();
                    var personOvcStatusManager = new PersonOvcStatusManager();
                    var personLookUpManager = new PersonLookUpManager();
                    PersonLookUp Guardian;
                    PatientLookup patient = new PatientLookup();

                    PersonId = Convert.ToInt32(Session["PersonId"]);

                    if (PersonId > 0)
                    {

                    }
                    else
                    {
                        patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                        if (null!=patient)
                        {
                            PersonId = patient.PersonId;
                        }
                    }

                    var personOVC = personOvcStatusManager.GetSpecificPatientOvcStatus(PersonId);

                    if (personOVC != null)
                    {
                        Guardian = personLookUpManager.GetPersonById(personOVC.GuardianId);
                        if (Guardian != null)
                        {
                            personLogic.UpdatePerson(firstname, middlename, lastname, gender, userId, Guardian.Id);
                            Session["PersonGuardianId"] = Guardian.Id;

                            Msg = "<p>Updated Guardian Successfully</p>";

                            var ovcStatus = new PersonOvcStatusManager();
                            PatientOVCStatus patientOvcStatus = ovcStatus.GetOvcByPersonAndGuardian(PersonId, Guardian.Id);
                            if (patientOvcStatus != null)
                            {
                                patientOvcStatus.Orphan = _orphan;
                                patientOvcStatus.InSchool = _inSchool;

                                Result = ovcStatus.UpdatePatientOvcStatus(patientOvcStatus);

                                Msg += "<p>Updated Person Ovc Status<p>";
                            }
                        }
                    }
                    else
                    {
                        int guardianId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, userId);
                        var ovcStatus = new PersonOvcStatusManager();
                        var patientovc = ovcStatus.AddPatientOvcStatus(PersonId, guardianId, _orphan, _inSchool, userId);

                        if (patientovc > 0)
                        {
                            Msg += "<p>Added Person Ovc Status<p>";
                        }
                    }
                }
                else
                {
                    var personLogic = new PersonManager();
                    PersonGuardianId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, userId);
                    Session["PersonGuardianId"] = PersonGuardianId;
                    if (PersonGuardianId > 0)
                    {
                        Msg = "<p>New Guardian Person Added successfully : GuardianId=> " + PersonGuardianId + "</p>";

                        PersonId = Convert.ToInt32(Session["PersonId"]);
                        var ovcStatus = new PersonOvcStatusManager();
                        Result = ovcStatus.AddPatientOvcStatus(PersonId, PersonGuardianId, _orphan, _inSchool, userId);
                        if (Result > 0)
                        {
                            Msg += "<p>Person Child OVC Status Recorded Successfully</p>";
                        }
                    }
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }

            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonLocation(int personId, int county, int subcounty, int ward, string village, string location, string sublocation, string landmark, string nearesthealthcentre,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var PatientId = Convert.ToInt32(Session["PatientPK"]);

                if (PersonId > 0 || PatientId > 0)
                {
                    var personLocation = new PersonLocationManager();
                    if (PersonId == 0)
                    {
                        var patientLogic = new PatientLookupManager();
                        var patient = patientLogic.GetPatientDetailSummary(PatientId);
                        PersonId = patient.PersonId;
                    }
                    

                    var currentLocation = personLocation.GetCurrentPersonLocation(PersonId);
                    if (currentLocation.Count > 0)
                    {
                        /*Update old location*/
                        currentLocation[0].DeleteFlag = true;
                        personLocation.UpdatePersonLocation(currentLocation[0]);
                        /*Add new location*/
                        Result = personLocation.AddPersonLocation(PersonId, county, subcounty, ward, village, location,
                            sublocation, landmark, nearesthealthcentre, userId);
                        if (Result > 0)
                        {
                            Msg += "<p>Person Location successfully updated</p>";
                        }
                    }
                    else
                    {
                        Result = personLocation.AddPersonLocation(PersonId, county, subcounty, ward, village, location,
                            sublocation, landmark, nearesthealthcentre, userId);
                        if (Result > 0)
                        {
                            Msg += "<p>Current Person Location Addedd successfully during !</p>";
                        }
                    }
                }
                else
                    Msg += "The current person was not updated";
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonContact(int personId,string physicalAddress,string mobileNumber,string alternativeNumber,string emailAddress,int userId, string patientid)
        {
            patientid = patientid == "null" ? null : patientid;
            patientid = patientid == "" ? null : patientid;


            try
            {
                int personContactId = 0;
                if(Session["PersonContactId"] != null)
                {
                    personContactId = int.Parse(Session["PersonContactId"].ToString());
                }
                
                if ((personContactId>0) || (patientid != null && int.Parse(patientid) > 0))
                {
                    PersonId = Convert.ToInt32(Session["PersonId"]);
                    var personContact = new PersonContactManager();
                    var personContactLookUp = new PersonContactLookUpManager();

                    if (PersonId == 0)
                    {
                        var patientLogic = new PatientLookupManager();
                        var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                        PersonId = patient.PersonId;
                    }

                    var contacts = personContactLookUp.GetPersonContactByPersonId(PersonId);

                    if (contacts.Count > 0)
                    {
                        //if (alternativeNumber != null)
                        //{
                        //    alternativeNumber = (alternativeNumber);
                        //}
                        //if (emailAddress != null)
                        //{
                        //    emailAddress = (emailAddress);
                        //}

                        PersonContact perContact = new PersonContact();
                        perContact.Id = contacts[0].Id;
                        perContact.PersonId = contacts[0].PersonId;
                        perContact.PhysicalAddress = (physicalAddress);
                        perContact.MobileNumber = (mobileNumber);
                        perContact.AlternativeNumber = alternativeNumber;
                        perContact.EmailAddress = emailAddress;

                        Session["PersonContactId"] = personContact.UpdatePatientContact(perContact);
                        Msg += "<p>Updated Person Contact Successfully.</p>";
                    }
                    else
                    {
                        var Result = personContact.AddPersonContact(PersonId, physicalAddress,
                            mobileNumber, alternativeNumber, emailAddress, userId);
                        Session["PersonContactId"] = Result;
                        if (Result > 0)
                        {
                            Msg += "<p>Person Contact Updated successfully!</p>";
                        }

                    }
                }
                else
                {
                    PersonId = Convert.ToInt32(Session["PersonId"]);
                    var personContact = new PersonContactManager();
                    Result = personContact.AddPersonContact(PersonId, physicalAddress, mobileNumber, alternativeNumber,
                        emailAddress, userId);
                    Session["PersonContactId"] = Result;
                    if (Result > 0)
                    {
                        Msg += "<p>Person Contact Added successuly!</p>";
                    }
                }
            }
            catch (SoapException exception)
            {
                Msg = exception.Message;
            }

            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender ,string nationalId,int userId, string mobileContact, string patientid, string supporterIsGuardian)
        {
            try
            {
                patientid = patientid == "null" ? null : patientid;
                patientid = patientid == "" ? null : patientid;

                int supporterId = 0;
                if (Session["PersonTreatmentSupporterId"] != null)
                {
                    supporterId = int.Parse(Session["PersonTreatmentSupporterId"].ToString());
                }

                if (supporterId>0 || (patientid != null && int.Parse(patientid) > 0))
                {
                    var personLogic = new PersonManager();
                    var patientLogic = new PatientLookupManager();
                    var patientTreatmentSupporter = new PatientTreatmentSupporterManager();
                    var patientTreatmentlookupManager = new PatientTreatmentSupporterLookupManager();
                    int personId = 0;

                    if (supporterId > 0)
                    {
                        personId = Convert.ToInt32(Session["PersonId"]);
                    }
                    else
                    {
                        var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                        personId = patient.PersonId;
                    }
                    
                    //var listPatientTreatmentSupporter = patientTreatmentSupporter.GetPatientTreatmentSupporter(personId);
                    var listPatientTreatmentSupporter = patientTreatmentlookupManager.GetAllPatientTreatmentSupporter(personId);

                    if (listPatientTreatmentSupporter.Count > 0)
                    {
                        personLogic.UpdatePerson(firstname, middlename, lastname, gender, userId,
                            listPatientTreatmentSupporter[0].SupporterId);

                        Session["PersonTreatmentSupporterId"] = listPatientTreatmentSupporter[0].SupporterId;

                        if (listPatientTreatmentSupporter[0].SupporterId > 0)
                        {
                            var treatmentSupporterManager = new PatientTreatmentSupporterManager();
                            //var treatmentSupporter = treatmentSupporterManager.GetPatientTreatmentSupporter(personId);
                            var treatmentSupporter = patientTreatmentlookupManager.GetAllPatientTreatmentSupporter(personId);
                            if (treatmentSupporter.Count > 0)
                            {
                                //treatmentSupporter[0].PersonId = personId;
                                //treatmentSupporter[0].SupporterId = listPatientTreatmentSupporter[0].SupporterId;
                                //treatmentSupporter[0].MobileContact = mobileContact;

                                PatientTreatmentSupporter supporter = new PatientTreatmentSupporter()
                                {
                                    Id = treatmentSupporter[0].Id,
                                    PersonId = personId,
                                    SupporterId = listPatientTreatmentSupporter[0].SupporterId,
                                    MobileContact = mobileContact,
                                    CreatedBy = treatmentSupporter[0].CreatedBy,
                                    DeleteFlag = treatmentSupporter[0].DeleteFlag
                                };

                                treatmentSupporterManager.UpdatePatientTreatmentSupporter(supporter);

                                Msg += "<p>Person Treatement Supported Updated Successfully</p>";
                            }
                        }
                        Msg += "<p>Person Treatment Supporter Updated Successfully.</p>";
                    }
                    else
                    {
                        if (supporterIsGuardian == "Yes")
                        {
                            PersonTreatmentSupporterId = int.Parse(Session["PersonGuardianId"].ToString());
                        }
                        else
                        {
                            PersonTreatmentSupporterId = personLogic.AddPersonTreatmentSupporterUiLogic(firstname,
                                middlename,
                                lastname, gender, userId);
                            Session["PersonTreatmentSupporterId"] = PersonTreatmentSupporterId;
                        }

                        if (PersonTreatmentSupporterId > 0)
                        {
                            Msg += "<p>New Treatment Supporter Person Added Successfully!</p>";

                            var treatmentSupporter = new PatientTreatmentSupporterManager();
                            Result = treatmentSupporter.AddPatientTreatmentSupporter(Convert.ToInt32(Session["PersonId"]), PersonTreatmentSupporterId,
                                mobileContact, userId);
                            if (Result > 0)
                            {
                                Msg += "<p>Person Treatement Supported Addeded Successfully!</p>";
                            }
                        }
                    }
                }
                else
                {
                    PersonId = Convert.ToInt32(Session["PersonId"]);

                    var personLogic = new PersonManager();

                    if (supporterIsGuardian == "Yes")
                    {
                        PersonTreatmentSupporterId = int.Parse(Session["PersonGuardianId"].ToString());
                    }
                    else
                    {
                        PersonTreatmentSupporterId = personLogic.AddPersonTreatmentSupporterUiLogic(firstname,
                            middlename,
                            lastname, gender, userId);
                        Session["PersonTreatmentSupporterId"] = PersonTreatmentSupporterId;
                    }

                    if (PersonTreatmentSupporterId > 0)
                    {
                        Msg += "<p>New Treatment Supporter Person Added Successfully!</p>";

                        var treatmentSupporter = new PatientTreatmentSupporterManager();
                        Result = treatmentSupporter.AddPatientTreatmentSupporter(PersonId, PersonTreatmentSupporterId,
                            mobileContact, userId);
                        if (Result > 0)
                        {
                            Msg += "<p>Person Treatement Supported Added Successfully!</p>";
                        }
                    }
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }

            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonRelationship(PersonRelationship relationship)
        {
            try
            {
                var personRelationship=new PersonRelationshipManager();
                Result = personRelationship.AddPersonRelationship(PersonId, relationship.RelatedTo, relationship.RelationshipTypeId);
                if (Result > 0)
                {
                    Msg = "PersonRelationship Added successfully!";
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }

            return Msg;
        }

        [WebMethod(EnableSession = true )]
        public string AddPersonPopulation(int personId, string populationtypeId,int populationCategory,int userId, string patientId)
        {
            try
            {
                patientId = patientId == "null" ? null : patientId;
                patientId = patientId == "" ? null : patientId;

                //(patientId != null && int.Parse(patientId) > 0)

                PersonId = Convert.ToInt32(Session["PersonId"]);
                if (!(PersonId > 0))
                {
                    var patientLogic = new PatientLookupManager();
                    var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientId));
                    PersonId = patient.PersonId;
                }

                var personPoulation = new PatientPopulationManager();
                var population = personPoulation.GetCurrentPatientPopulations(PersonId);
                if (population.Count > 0)
                {
                    population[0].PopulationCategory = populationCategory;
                    population[0].PopulationType = populationtypeId;

                    personPoulation.UpdatePatientPopulation(population[0]);

                    Msg += "<p>Person Population Edited Successfully.</p>";

                }
                else
                {
                    Result = personPoulation.AddPatientPopulation(PersonId, populationtypeId, populationCategory, userId);
                    if (Result > 0)
                    {
                        Msg += "<p>Person Population Status Recorded Successfully!</p>";
                    }
                }
            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetGuardian()
        {
            try
            {
                int guardianId = int.Parse(Session["PersonGuardianId"].ToString());
                var personLookUpManager = new PersonLookUpManager();
                var guardian = personLookUpManager.GetPersonById(guardianId);
                if (guardian!=null)
                {
                    PatientDetails patientDetails = new PatientDetails();
                    patientDetails.FirstName = (guardian.FirstName);
                    patientDetails.MiddleName = (guardian.MiddleName);
                    patientDetails.LastName = (guardian.LastName);
                    patientDetails.Gender = guardian.Sex;


                    return new JavaScriptSerializer().Serialize(patientDetails);
                }

            }
            catch (SoapException e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetPersonDetails(int PatientId)
        {
            PatientDetails patientDetails = new PatientDetails();
            try
            {
                var patientLookManager = new PatientLookupManager();
                var personOvcStatusManager = new PersonOvcStatusManager();
                var personLookUpManager = new PersonLookUpManager();
                var personMaritalStatus = new PersonMaritalStatusManager();
                var lookupLogic = new LookupLogic();
                //PersonLookUp Guardian = new PersonLookUp();
                PersonLookUp supporter = new PersonLookUp();
                var maritalsStatus = new List<PatientMaritalStatus>();
                var personLocation = new PersonLocationManager();
                var personContacts = new List<PersonContactLookUp>();
                var personContactLookUpManager = new PersonContactLookUpManager();
                var patientTreatmentSupporterManager = new PatientTreatmentSupporterManager();
                var patientTreatmentSupporterLookupManager = new PatientTreatmentSupporterLookupManager();
                var patientTreatmentSupporter = new List<PatientTreatmentSupporterLookup>();
                var keyPopulationManager = new PatientPopulationManager();
                var keyPopulation = new List<PatientPopulation>();
                var patientEntryPoint = new PatientEntryPointManager();
                var entryPoints = new List<PatientEntryPoint>();

            PatientLookup thisPatient = patientLookManager.GetPatientDetailSummary(PatientId);

                if (null!= thisPatient)
                {
                    var personOVC = personOvcStatusManager.GetSpecificPatientOvcStatus(thisPatient.PersonId);
                    var perLocation = personLocation.GetCurrentPersonLocation(thisPatient.PersonId);

                    PersonLookUp Guardian = null;
                    if (personOVC != null)
                        Guardian = personLookUpManager.GetPersonById(personOVC.GuardianId);
                    maritalsStatus = personMaritalStatus.GetAllMaritalStatuses(thisPatient.PersonId);
                    personContacts = personContactLookUpManager.GetPersonContactByPersonId(thisPatient.PersonId);
                    /*patientTreatmentSupporter =
                        patientTreatmentSupporterManager.GetAllPatientTreatmentSupporter(thisPatient.PersonId);*/
                    patientTreatmentSupporter = patientTreatmentSupporterLookupManager.GetAllPatientTreatmentSupporter(thisPatient.PersonId);
                keyPopulation = keyPopulationManager.GetAllPatientPopulations(thisPatient.PersonId);
                    entryPoints = patientEntryPoint.GetPatientEntryPoints(thisPatient.Id);

                    patientDetails.FirstName = (thisPatient.FirstName);
                    patientDetails.MiddleName = (thisPatient.MiddleName);
                    patientDetails.LastName = (thisPatient.LastName);
                    patientDetails.PatientType = thisPatient.PatientType;
                    patientDetails.PatientTypeString = LookupLogic.GetLookupNameById(thisPatient.PatientType);
                    patientDetails.EnrollmentNumber = thisPatient.EnrollmentNumber;

                    patientDetails.Gender = thisPatient.Sex;
                    patientDetails.GenderString = LookupLogic.GetLookupNameById(thisPatient.Sex);
                    patientDetails.PersonDoB = String.Format("{0:dd-MMM-yyyy}", thisPatient.DateOfBirth);
                    patientDetails.EnrollmentDate = String.Format("{0:dd-MMM-yyyy}", thisPatient.EnrollmentDate);
                    patientDetails.Age = patientDetails.GetAge(thisPatient.DateOfBirth);

                    //OVC
                    if (personOVC != null && personOVC.Orphan)
                    {
                        var item = lookupLogic.GetItemIdByGroupAndItemName("YesNo", "Yes");
                        if (null != item && item.Count > 0)
                        {
                            patientDetails.ChildOrphan = item[0].ItemId;
                            patientDetails.ChildOrphanString = "Yes";
                        }
                    }
                    else
                    {
                        var item = lookupLogic.GetItemIdByGroupAndItemName("YesNo", "No");
                        if (null != item && item.Count > 0)
                        {
                            patientDetails.ChildOrphan = item[0].ItemId;
                            patientDetails.ChildOrphanString = "No";
                        }
                    }

                    patientDetails.Inschool = (personOVC != null && personOVC.InSchool)
                        ? lookupLogic.GetItemIdByGroupAndItemName("YesNo", "Yes")[0].ItemId
                        : lookupLogic.GetItemIdByGroupAndItemName("YesNo", "No")[0].ItemId;

                    patientDetails.InschoolString = (personOVC != null && personOVC.InSchool)
                        ? "Yes"
                        : "No";

                    patientDetails.NationalId = (thisPatient.NationalId);

                    if (maritalsStatus.Count > 0)
                    {
                        patientDetails.MaritalStatusId = maritalsStatus[0].MaritalStatusId;
                        patientDetails.MaritalStatusString =
                            LookupLogic.GetLookupNameById(maritalsStatus[0].MaritalStatusId);
                    }

                    if (Guardian != null)
                    {
                        patientDetails.GurdianFNames = (Guardian.FirstName);
                        patientDetails.GurdianMName = (Guardian.MiddleName);
                        patientDetails.GurdianLName = (Guardian.LastName);

                        patientDetails.GuardianGender = Guardian.Sex;
                        patientDetails.GuardianId = Guardian.Id;
                    }

                    //Location
                    if (perLocation.Count > 0)
                    {
                        patientDetails.CountyId = perLocation[0].County;
                        patientDetails.SubCounty = perLocation[0].SubCounty;
                        patientDetails.Ward = perLocation[0].Ward;
                        patientDetails.Village = perLocation[0].Village;
                        patientDetails.Location = perLocation[0].Location;
                        patientDetails.SubLocation = perLocation[0].SubLocation;
                        patientDetails.LandMark = perLocation[0].LandMark;
                        patientDetails.NearestHealthCentre = perLocation[0].NearestHealthCentre;
                    }
                    //Person Contacts
                    if (personContacts.Count > 0)
                    {
                        patientDetails.PatientPostalAddress = (personContacts[0].PhysicalAddress);
                        patientDetails.MobileNumber = (personContacts[0].MobileNumber);
                        patientDetails.AlternativeNumber = (personContacts[0].AlternativeNumber);
                        patientDetails.EmailAddress = (personContacts[0].EmailAddress);
                    }
                    //Treatment Supporter
                    if (patientTreatmentSupporter.Count > 0)
                    {
                        supporter = personLookUpManager.GetPersonById(patientTreatmentSupporter[0].SupporterId);
                        if (supporter != null)
                        {
                            patientDetails.tsFname = (supporter.FirstName);
                            patientDetails.tsMiddleName = (supporter.MiddleName);
                            patientDetails.tsLastName = (supporter.LastName);
                            patientDetails.tsGender = supporter.Sex;
                            patientDetails.ISContacts = Convert.ToString(patientTreatmentSupporter[0].MobileContact);
                            patientDetails.PatientTreatmentSupporterId = supporter.Id;
                        }
                        
                    }
                    //Key Population
                    if (keyPopulation.Count > 0)
                    {
                        patientDetails.population = keyPopulation[0].PopulationType;
                        patientDetails.PopulationCategoryId = keyPopulation[0].PopulationCategory;
                        patientDetails.PopulationCategoryString =
                            LookupLogic.GetLookupNameById(keyPopulation[0].PopulationCategory);

                        if (keyPopulation[0].PopulationType == "General Population")
                        {
                            var items = lookupLogic.GetItemIdByGroupAndItemName("PopulationType",
                                "Gen.Pop");
                            if (items.Count > 0)
                            {
                                patientDetails.populationTypeId = items[0].ItemId;
                            }
                            
                        }
                        else
                        {
                            var items = lookupLogic.GetItemIdByGroupAndItemName("PopulationType",
                                "Key.Pop");
                            if (items.Count > 0)
                            {
                                patientDetails.populationTypeId = items[0].ItemId;
                            }
                        }
                    }
                    //Entry Point
                    if (entryPoints.Count > 0)
                    {
                        patientDetails.EntryPoint = LookupLogic.GetLookupNameById(entryPoints[0].EntryPointId);
                    }
                }

                return new JavaScriptSerializer().Serialize(patientDetails);
            }
            catch (SoapException e)
            {
                return e.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientSearchresults(string firstName,string middleName,string lastName, string dob)
        {
            try
            {
                var personLookUpManager = new PersonLookUpManager();
                //var dobb = "";

                var results = personLookUpManager.GetPersonSearchResults(firstName, middleName, lastName, dob);
                var patientLookup = new PatientLookupManager();
                
                var newresults = results.Select(x => new string[]
                   {
                        x.Id.ToString(),
                        (x.FirstName),
                        (x.MiddleName),
                        (x.LastName),
                        patientLookup.GetDobByPersonId(x.Id),
                        LookupLogic.GetLookupNameById(x.Sex),
                        patientLookup.IsPatientExists(x.Id).ToString(),
                        patientLookup.PatientId(x.Id).ToString()
                   });

                return new JavaScriptSerializer().Serialize(newresults);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string SetSession(int personId)
        {
            if (personId > 0)
            {
                Session["PersonId"] = personId;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public void GetPatientType(string groupName, string patientTypeName)
        {
            try
            {
                var patientTypeId = 0;

                var lookUpLogic = new LookupLogic();
                var patientTypes = lookUpLogic.GetItemIdByGroupAndItemName(groupName, patientTypeName);
                if (patientTypes.Count > 0)
                {
                    patientTypeId = patientTypes[0].ItemId;
                }

                Session["PatientType"] = patientTypeId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
