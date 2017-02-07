using System;
using System.Web.Services;
using Entities.Common;
using Entities.PatientCore;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.WebService
{
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

        [WebMethod(EnableSession = true)]
        public string AddPerson(string firstname, string middlename, string lastname, int gender,DateTime dateOfBirth, string nationalId, int userId)
        {
            try
            {
                
                var personLogic = new PersonManager();

                PersonId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender,dateOfBirth, nationalId, userId);
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
        public string AddPersonMaritalStatus(int personId,int maritalStatusId,int userId)
        {
            try
            {
                PersonId =Convert.ToInt32(Session["personId"]);
                var maritalStatus = new PersonMaritalStatusManager();
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
        public string AddPersonGuardian(string firstname, string middlename, string lastname, int gender,DateTime dateOfBirth, string nationalId, int userId)
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
        public string AddPersonOvcStatus(int personId,int guardianId,string orphan,string inSchool,int userId)
        {
            bool _orphan;
            bool _inSchool;

            if (orphan == "yes") { _orphan = true; } else { _orphan = false; }
            if (inSchool == "yes") { _inSchool = true; } else { _inSchool = false; }
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
        public string AddPersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender,DateTime dateOfBirth ,string nationalId,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
               
                var personLogic = new PersonManager();
                PersonTreatmentSupporterId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender,dateOfBirth ,nationalId, userId);
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
        public string AddTreatmentSupporter(int personId,int supporterId,int mobileContact,int userId)
        {
            try
            {
                PersonId = Convert.ToInt32(Session["PersonId"]);
                var treatmentSupporter=new PatientTreatmentSupporterManager();
                Result = treatmentSupporter.AddPatientTreatmentSupporter(PersonId, supporterId, mobileContact, userId);
                if (Result > 0)
                {
                    Msg = "Person Treatement Supported Addedded successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' + e.InnerException;
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

    }
}
