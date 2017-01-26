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
        private int _personId;
        private int _personGuardianId;
        private int _personTreatmentSupporterId;
        private string _msg;
        private int _result;

        [WebMethod]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
            try
            {
                var personLogic = new PersonManager();

                _personId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId, userId);

                if (_personId > 0)
                {
                    _msg = "New Person Added Successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            
            return _msg;
        }

        [WebMethod]
        public string AddPersonMaritalStatus(int patientId,int maritalStatusId,int userId)
        {
            try
            {
                var maritalStatus = new PersonMaritalStatusManager();
                _result = maritalStatus.AddPatientMaritalStatus(_personId, maritalStatusId,userId);
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
                    _msg = "New Guardian Person Added Successfully!";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message;
            }
            return _msg;
        }

        [WebMethod]
        public string AddPersonOvcStatus(int personid,int guardianId,Boolean orphan,Boolean inSchool,int userId)
        {
            PatientOVCStatus patientOvcStatus=new PatientOVCStatus()
            {
               
            };
            try
            {
                var ovcStatus = new PersonOvcStatusManager();
                _result = ovcStatus.AddPatientOvcStatus(_personId, _personGuardianId, orphan, inSchool, userId);

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
               _result= personLocation.AddPersonLocation(_personId,county,subCounty,ward,village,estate,landmark,nearestHealthCentre);
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
                _result = personContact.AddPersonContact(_personId, physicalAddress, mobileNumber);
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
        public string AddPersonPopulation(PatientPopulation patientPopulation)
        {
            try
            {
                var personOvcStatus = new PersonOvcStatusManager();
                _result = personOvcStatus.AddPatientOvcStatus(_personId, patientPopulation.PopulationTypeId, patientPopulation.PopulationCategory, patientPopulation.CreatedBy);
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
