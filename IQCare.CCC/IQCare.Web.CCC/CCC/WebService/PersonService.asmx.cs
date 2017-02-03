using System;
using System.Web.Services;
using Entities.Common;
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

        [WebMethod]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
            try
            {
                var personLogic = new PersonManager();

                PersonId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId, userId);

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

        [WebMethod]
        public string AddPersonMaritalStatus(int personId,int maritalStatusId,int userId)
        {
            try
            {
                var maritalStatus = new PersonMaritalStatusManager();
                Result = maritalStatus.AddPatientMaritalStatus(PersonId, maritalStatusId,userId);
                if (Result > 0)
                {
                    Msg = "Person Marital Status Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message+' '+e.InnerException;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPersonGuardian(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
            try
            {
                var personLogic = new PersonManager();
                PersonGuardianId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId, userId);
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

        [WebMethod]
        public string AddPersonOvcStatus(int personId,int guardianId,string orphan,string inSchool,int userId)
        {
            bool _orphan;
            bool _inSchool;

            if (orphan == "yes") { _orphan = true; } else { _orphan = false; }
            if (inSchool == "yes") { _inSchool = true; } else { _inSchool = false; }
            try
            {
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
        [WebMethod]
        public string AddPersonLocation(int personId,int county,int subCounty,int ward,string village,string estate,string landmark,string nearestHealthCentre)
        {
            try
            {
                var personLocation = new PersonLocationManager();
               Result= personLocation.AddPersonLocation(personId, county,subCounty,ward,village,estate,landmark,nearestHealthCentre);
               if(Result>0) { Msg = "Person Location Addedd successfully!";}
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPersonContact(int personId,string physicalAddress,string mobileNumber)
        {
            try
            {
                var personContact = new PersonContactManager();
                Result = personContact.AddPersonContact(personId, physicalAddress, mobileNumber);
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

        [WebMethod]
        public string AddPersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender,string nationalId,int userId)
        {
            try
            {
                var personLogic = new PersonManager();
                PersonTreatmentSupporterId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender,nationalId, userId);
                if (PersonTreatmentSupporterId > 0)
                {
                    Msg = "New Treatment Supporter Person Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }


            return Msg;
        }

        [WebMethod]
        public string AddPersonRelationship(PersonRelationship relationship)
        {
            try
            {
                var personRelationship=new PersonRelationshipManager();
                Result = personRelationship.AddPersonRelationship(PersonId, relationship.RelatedTo,
                    relationship.RelationshipTypeId);
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

        [WebMethod]
        public string AddPersonPopulation(int patientId,int populationtypeId,int populationCategory,int userId)
        {
            try
            {
                var personPoulation = new PatientPopulationManager();
                Result = personPoulation.AddPatientPopulation(patientId, populationtypeId, populationCategory, userId);
                if (Result > 0)
                {
                    Msg = "Person OVC Status Recorded Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

    }
}
