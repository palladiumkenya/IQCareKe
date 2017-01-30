using System;
using System.Web.Services;
using Entities.Common;
using IQCare.CCC.UILogic;
using Entities.PatientCore;

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
        private int _personId { get; set; }
        private int _personGuardianId { get; set; }
        private int _personTreatmentSupporterId { get; set; }
        private string _msg { get; set; }
        private int _result { get; set; }

        [WebMethod]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
            try
            {
                var personLogic = new PersonManager();

                _personId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId, userId);

                if (_personId > 0)
                {
                    _msg = ""+ _personId +"";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            
            return _msg;
        }

        [WebMethod]
        public string AddPersonMaritalStatus(int personId,int maritalStatusId,int userId)
        {
            try
            {
                var maritalStatus = new PersonMaritalStatusManager();
                _result = maritalStatus.AddPatientMaritalStatus(personId, maritalStatusId,userId);
                if (_result > 0)
                {
                    _msg = "Person Marital Status Added Successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
        }

        [WebMethod]
        public string AddPersonGuardian(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
            try
            {
                var personLogic = new PersonManager();
                _personGuardianId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId, userId);
                if (_personGuardianId > 0)
                {
                    _msg= _personGuardianId.ToString();
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
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
                _result = ovcStatus.AddPatientOvcStatus(personId, guardianId, _orphan, _inSchool, userId);

            }
            catch (Exception e)
            {
                this._msg = e.Message;
            }
            return _msg; 
        }
        [WebMethod]
        public string AddPersonLocation(int personId,int county,int subCounty,int ward,string village,string estate,string landmark,string nearestHealthCentre)
        {
            try
            {
                var personLocation = new PersonLocationManager();
               _result= personLocation.AddPersonLocation(personId, county,subCounty,ward,village,estate,landmark,nearestHealthCentre);
               if(_result>0) { _msg = "Person Location Addedd successfully!";}
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
        }

        [WebMethod]
        public string AddPersonContact(int personId,string physicalAddress,string mobileNumber)
        {
            try
            {
                var personContact = new PersonContactManager();
                _result = personContact.AddPersonContact(personId, physicalAddress, mobileNumber);
                if (_result > 0)
                {
                    _msg = "Person Contact Addedd successuly!";
                }
            }
            catch (Exception exception)
            {
                _msg = exception.Message;
            }
            return _msg;
        }

        [WebMethod]
        public string AddPersonTreatmentSupporter(string firstname, string middlename, string lastname, int gender,string nationalId,int userId)
        {
            try
            {
                var personLogic = new PersonManager();
                _personTreatmentSupporterId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender,nationalId, userId);
                if (_personTreatmentSupporterId > 0)
                {
                    _msg = "New Treatment Supporter Person Added Successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }


            return _msg;
        }

        [WebMethod]
        public string AddPersonRelationship(PersonRelationship relationship)
        {
            try
            {
                var personRelationship=new PersonRelationshipManager();
                _result = personRelationship.AddPersonRelationship(_personId, relationship.RelatedTo,
                    relationship.RelationshipTypeId);
                if (_result > 0)
                {
                    _msg = "PersonRelationship Added successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
        }

        [WebMethod]
        public string AddPersonPopulation(int patientId,int populationtypeId,int populationCategory,int userId)
        {
            try
            {
                var personPoulation = new PatientPopulationManager();
                _result = personPoulation.AddPatientPopulation(patientId, populationtypeId, populationCategory, userId);
                if (_result > 0)
                {
                    _msg = "Person OVC Status Recorded Successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
        }

    }
}
