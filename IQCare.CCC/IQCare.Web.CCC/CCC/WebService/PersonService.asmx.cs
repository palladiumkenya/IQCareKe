using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;
using Entities.CCC.Enrollment;
using Entities.Common;
using Entities.PatientCore;
using IQCare.CCC.UILogic;
using System.Web.Script.Serialization;
using Application.Common;
using Entities.CCC.Lookup;

namespace IQCare.Web.CCC.WebService
{
    public class PatientDetails
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string PersonDoB { get; set; }
        public int ChildOrphan { get; set; }
        public int Inschool { get; set; }
        public string NationalId { get; set; }
        public int MaritalStatusId { get; set; }
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
        Utility _utility = new Utility();
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        [WebMethod(EnableSession = true)]
        public string AddPerson(string firstname, string middlename, string lastname, int gender,string dateOfBirth, string nationalId, int userId, string patientid)
        {
            try
            {
                
                var personLogic = new PersonManager();
                var dob = DateTime.Parse(dateOfBirth);

                PersonId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, dob, nationalId, userId);
                Session["PersonId"] = PersonId;
                if (PersonId > 0)
                {
                    Msg = "New Person Added successfully : PersonId=> "+PersonId;
                }
            }
            catch (Exception e)
            {
                Msg = e.Message+' '+ e.InnerException;
            }
            
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePerson(string firstname, string middlename, string lastname, int gender, string dateOfBirth, string nationalId, int userId, string patientid)
        {
            try
            {
                var personManager = new PersonManager();
                var patientLogic = new PatientLookupManager();
                var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                int personId = patient[0].PersonId;

                Person person = new Person()
                {
                    FirstName = _utility.Encrypt(_textInfo.ToTitleCase(firstname)),
                    MidName = _utility.Encrypt(_textInfo.ToTitleCase(middlename)),
                    LastName = _utility.Encrypt(_textInfo.ToTitleCase(lastname)),
                    Sex = gender,
                    DateOfBirth = DateTime.Parse(dateOfBirth),
                    NationalId = _utility.Encrypt(nationalId),
                    CreatedBy = userId
                };
                personManager.UpdatePerson(person, personId);
                Session["PersonId"] = personId;

                Msg = "Person Updated successfully";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonMaritalStatus(int personId,int maritalStatusId,int userId, string patientid)
        {
            try
            {
                PersonId =Convert.ToInt32(Session["PersonId"]);
                var maritalStatus = new PersonMaritalStatusManager();
                var lookUpLogic = new LookupLogic();

                if (maritalStatusId == 0)
                {
                    maritalStatusId = lookUpLogic.GetItemIdByGroupAndItemName("MaritalStatus", "Single")[0].ItemId;
                }

                Result = maritalStatus.AddPatientMaritalStatus(PersonId, maritalStatusId,userId);
                if (Result > 0)
                {
                    Msg = "Person Marital Status Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message+' '+ e.InnerException;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonMaritalStatus(int personId, int maritalStatusId, int userId, string patientid)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var maritalStatus = new PersonMaritalStatusManager();
                var _matStatus = maritalStatus.GetInitialPatientMaritalStatus(PersonId);

                int matStatusId;
                var lookUpLogic = new LookupLogic();
                matStatusId = maritalStatusId > 0 ? maritalStatusId : lookUpLogic.GetItemIdByGroupAndItemName("MaritalStatus", "Single")[0].ItemId;

                _matStatus.MaritalStatusId = matStatusId;
                _matStatus.CreatedBy = userId;

                Result = maritalStatus.UpdatePatientMaritalStatus(_matStatus);
                if (Result > 0)
                {
                    Msg = "Person Marital Status Updated Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPersonGuardian(string firstname, string middlename, string lastname, int gender,DateTime dateOfBirth, string nationalId, int userId, string patientid)
        {
            try
            {
                var personLogic = new PersonManager();
                PersonGuardianId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender,dateOfBirth, nationalId, userId);
                Session["PersonGuardianId"] = PersonGuardianId;
                if (PersonGuardianId > 0)
                {
                    Msg = "New Guardian Person Added successfully : GuardianId=>"+PersonGuardianId;
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonGuardian(string firstname, string middlename, string lastname, int gender, DateTime dateOfBirth, string nationalId, int userId, string patientid)
        {
            try
            {
                var personLogic = new PersonManager();
                var patientLogic = new PatientLookupManager();
                var personOvcStatusManager = new PersonOvcStatusManager();
                var personLookUpManager = new PersonLookUpManager();
                var Guardian = new List<PersonLookUp>();

                var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                if (patient.Count > 0)
                {
                    var personOVC = personOvcStatusManager.GetSpecificPatientOvcStatus(patient[0].PersonId);
                    if (personOVC != null)
                        Guardian = personLookUpManager.GetPersonById(personOVC.GuardianId);

                    if (Guardian.Count > 0)
                    {
                        Person person = new Person()
                        {
                            FirstName = _utility.Encrypt(_textInfo.ToTitleCase(firstname)),
                            MidName = _utility.Encrypt(_textInfo.ToTitleCase(middlename)),
                            LastName = _utility.Encrypt(_textInfo.ToTitleCase(lastname)),
                            Sex = gender,
                            DateOfBirth = dateOfBirth,
                            NationalId = _utility.Encrypt(nationalId)
                        };

                        personLogic.UpdatePerson(person, Guardian[0].Id);
                        Session["PersonGuardianId"] = Guardian[0].Id;

                        Msg = "Updated Guardian Successfully";
                    }
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string AddPersonOvcStatus(int personId,int guardianId,string orphan,string inSchool,int userId)
        {
            bool _orphan;
            bool _inSchool;

            if (orphan == "Yes") { _orphan = true; } else { _orphan = false; }
            if (inSchool == "Yes") { _inSchool = true; } else { _inSchool = false; }
            try
            {
                PersonGuardianId = Convert.ToInt32(Session["PersonGuardianId"]); 
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var ovcStatus = new PersonOvcStatusManager();
                Result = ovcStatus.AddPatientOvcStatus(PersonId, PersonGuardianId, _orphan, _inSchool, userId);
                if (Result > 0)
                {
                    Msg = "Person Child OVC Status Recorded Successfully .";
                }

            }
            catch (Exception e)
            {
                Msg = "Error Message: " + e.Message+' '+" Exception: "+e.InnerException;
            }
            return Msg; 
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonOvcStatus(int personId, int guardianId, string orphan, string inSchool, int userId)
        {
            bool _orphan;
            bool _inSchool;

            if (orphan == "Yes") { _orphan = true; } else { _orphan = false; }
            if (inSchool == "Yes") { _inSchool = true; } else { _inSchool = false; }

            try
            {
                PersonGuardianId = Convert.ToInt32(Session["PersonGuardianId"]);
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var ovcStatus = new PersonOvcStatusManager();

                PatientOVCStatus patientOvcStatus = ovcStatus.GetOvcByPersonAndGuardian(PersonId, PersonGuardianId);

                patientOvcStatus.Orphan = _orphan;
                patientOvcStatus.InSchool = _inSchool;

                Result = ovcStatus.UpdatePatientOvcStatus(patientOvcStatus);

                Msg = "Updated Person Ovc Status";
            }
            catch (Exception e)
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
                var personLocation = new PersonLocationManager();
               Result= personLocation.AddPersonLocation(PersonId, county,subcounty,ward,village,location,sublocation,landmark, nearesthealthcentre,userId);
               if(Result>0) { Msg = "Current Person Location Addedd successfully during !";}
            }
            catch (Exception e)
            {
                Msg = e.Message+ ' ' + e.InnerException;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonLocation(int personId, int county, int subcounty, int ward, string village, string location, string sublocation, string landmark, string nearesthealthcentre, int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var personLocation = new PersonLocationManager();

                var currentLocation =  personLocation.GetCurrentPersonLocation(PersonId);
                if (currentLocation.Count > 0)
                {
                    currentLocation[0].PersonId = PersonId;
                    currentLocation[0].County = county;
                    currentLocation[0].SubCounty = subcounty;
                    currentLocation[0].Ward = ward;
                    currentLocation[0].Village = village;
                    currentLocation[0].Location = location;
                    currentLocation[0].SubLocation = sublocation;
                    currentLocation[0].LandMark = landmark;
                    currentLocation[0].NearestHealthCentre = nearesthealthcentre;

                    personLocation.UpdatePersonLocation(currentLocation[0]);

                    Msg = "Person Location successfully updated";
                }

                
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string AddPersonContact(int personId,string physicalAddress,string mobileNumber,string alternativeNumber,string emailAddress,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var personContact = new PersonContactManager();
                Result = personContact.AddPersonContact(PersonId, physicalAddress, mobileNumber,alternativeNumber,emailAddress,userId);
                if (Result > 0)
                {
                    Msg = "Person Contact Addedd successuly!";
                }
            }
            catch (Exception exception)
            {
                Msg = exception.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonContact(int personId, string physicalAddress, string mobileNumber, string alternativeNumber, string emailAddress, int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var personContact = new PersonContactManager();

                if (alternativeNumber != null)
                {
                    alternativeNumber = _utility.Encrypt(alternativeNumber);
                }
                if (emailAddress != null)
                {
                    emailAddress = _utility.Encrypt(emailAddress);
                }

                PersonContact perContact = new PersonContact
                {
                    PersonId = personId,
                    PhysicalAddress = _utility.Encrypt(physicalAddress),
                    MobileNumber = _utility.Encrypt(mobileNumber),
                    AlternativeNumber = alternativeNumber,
                    EmailAddress = emailAddress
                };

                personContact.UpdatePatientContact(perContact);
                Msg = "Updated Person Contact Successfully";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string AddPersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender ,string nationalId,int userId, string patientid)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
               
                var personLogic = new PersonManager();
                PersonTreatmentSupporterId = personLogic.AddPersonTreatmentSupporterUiLogic(firstname, middlename, lastname, gender ,nationalId, userId);
                Session["PersonTreatmentSupporterId"] = PersonTreatmentSupporterId;

                if (PersonTreatmentSupporterId > 0)
                {
                    Msg = "New Treatment Supporter Person Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' +e.InnerException;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender, string nationalId, int userId, string patientid)
        {
            try
            {
                var personLogic = new PersonManager();
                var patientLogic = new PatientLookupManager();
                var patientTreatmentSupporter = new PatientTreatmentSupporterManager();
                
                var patient = patientLogic.GetPatientDetailSummary(int.Parse(patientid));
                int personId = patient[0].PersonId;
                var listPatientTreatmentSupporter = patientTreatmentSupporter.GetPatientTreatmentSupporter(personId);

                if (listPatientTreatmentSupporter.Count > 0)
                {

                    Person person = new Person()
                    {
                        FirstName = _utility.Encrypt(_textInfo.ToTitleCase(firstname)),
                        MidName = _utility.Encrypt(_textInfo.ToTitleCase(middlename)),
                        LastName = _utility.Encrypt(_textInfo.ToTitleCase(lastname)),
                        Sex = gender,
                        DateOfBirth = DateTime.Now,
                        NationalId = _utility.Encrypt(nationalId)
                    };
                    personLogic.UpdatePerson(person, listPatientTreatmentSupporter[0].SupporterId);
                    Session["PersonTreatmentSupporterId"] = listPatientTreatmentSupporter[0].SupporterId;

                    Msg = "Person Treatment Supporter Updated Successfully";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }

            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddTreatmentSupporter(int personId,int supporterId,int mobileContact,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                supporterId = Convert.ToInt32(Session["PersonTreatmentSupporterId"]);
                var treatmentSupporter=new PatientTreatmentSupporterManager();
                Result = treatmentSupporter.AddPatientTreatmentSupporter(PersonId, supporterId, mobileContact, userId);
                if (Result > 0)
                {
                    Msg = "Person Treatement Supported Addeded Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' + e.InnerException;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdateTreatmentSupporter(int personId, int supporterId, int mobileContact, int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                supporterId = Convert.ToInt32(Session["PersonTreatmentSupporterId"]);
                var treatmentSupporterManager = new PatientTreatmentSupporterManager();
                var treatmentSupporter =  treatmentSupporterManager.GetPatientTreatmentSupporter(PersonId);
                if (treatmentSupporter.Count > 0)
                {
                    treatmentSupporter[0].PersonId = PersonId;
                    treatmentSupporter[0].SupporterId = supporterId;
                    treatmentSupporter[0].MobileContact = mobileContact;

                    treatmentSupporterManager.UpdatePatientTreatmentSupporter(treatmentSupporter[0]);

                    Msg = "Person Treatement Supported Updated Successfully";
                }
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true )]
        public string AddPersonPopulation(int patientId,string populationtypeId,int populationCategory,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var personPoulation = new PatientPopulationManager();
                Result = personPoulation.AddPatientPopulation(PersonId, populationtypeId, populationCategory, userId);
                if (Result > 0)
                {
                    Msg = "Person OVC Status Recorded Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message+' '+ e.InnerException;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string UpdatePersonPopulation(int patientId, string populationtypeId, int populationCategory, int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var personPoulation = new PatientPopulationManager();
                //personPoulation.UpdatePatientPopulation()
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
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
                var Guardian = new List<PersonLookUp>();
                var maritalsStatus = new List<PatientMaritalStatus>();
                var personLocation = new PersonLocationManager();
                var personContacts = new List<PersonContactLookUp>();
                var personContactLookUpManager = new PersonContactLookUpManager();

                Patient = patientLookManager.GetPatientDetailSummary(PatientId);

                if (Patient.Count > 0)
                {
                    var personOVC = personOvcStatusManager.GetSpecificPatientOvcStatus(Patient[0].PersonId);
                    var perLocation = personLocation.GetCurrentPersonLocation(Patient[0].PersonId);

                    if (personOVC != null)
                        Guardian = personLookUpManager.GetPersonById(personOVC.GuardianId);
                    maritalsStatus = personMaritalStatus.GetAllMaritalStatuses(Patient[0].PersonId);
                    personContacts = personContactLookUpManager.GetPersonContactByPersonId(Patient[0].PersonId);

                    patientDetails.FirstName = _utility.Decrypt(Patient[0].FirstName);
                    patientDetails.MiddleName = _utility.Decrypt(Patient[0].MiddleName);
                    patientDetails.LastName = _utility.Decrypt(Patient[0].LastName);

                    patientDetails.Gender = Patient[0].Sex;
                    patientDetails.PersonDoB = String.Format("{0:dd-MMM-yyyy}", Patient[0].DateOfBirth);
                    patientDetails.Age = patientDetails.GetAge(Patient[0].DateOfBirth);

                    //OVC
                    if (personOVC != null && personOVC.Orphan)
                    {
                        var item = lookupLogic.GetItemIdByGroupAndItemName("YesNo", "Yes");
                        patientDetails.ChildOrphan = item[0].ItemId;
                    }
                    else
                    {
                        var item = lookupLogic.GetItemIdByGroupAndItemName("YesNo", "No");
                        patientDetails.ChildOrphan = item[0].ItemId;
                    }

                    patientDetails.Inschool = (personOVC != null && personOVC.InSchool)
                        ? lookupLogic.GetItemIdByGroupAndItemName("YesNo", "Yes")[0].ItemId
                        : lookupLogic.GetItemIdByGroupAndItemName("YesNo", "No")[0].ItemId;

                    patientDetails.NationalId = _utility.Decrypt(Patient[0].NationalId);

                    if (maritalsStatus.Count > 0)
                        patientDetails.MaritalStatusId = maritalsStatus[0].MaritalStatusId;

                    if (Guardian.Count > 0)
                    {
                        patientDetails.GurdianFNames = _utility.Decrypt(Guardian[0].FirstName);
                        patientDetails.GurdianMName = _utility.Decrypt(Guardian[0].MiddleName);
                        patientDetails.GurdianLName = _utility.Decrypt(Guardian[0].LastName);

                        patientDetails.GuardianGender = Guardian[0].Sex;
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
                        patientDetails.PatientPostalAddress = _utility.Decrypt(personContacts[0].PhysicalAddress);
                        patientDetails.MobileNumber = _utility.Decrypt(personContacts[0].MobileNumber);
                        patientDetails.AlternativeNumber = _utility.Decrypt(personContacts[0].AlternativeNumber);
                        patientDetails.EmailAddress = _utility.Decrypt(personContacts[0].EmailAddress);
                    }

                    return new JavaScriptSerializer().Serialize(patientDetails);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
