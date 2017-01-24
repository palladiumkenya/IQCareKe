using System;
using System.Web.Services;
using DataAccess.CCC.Repository.person;
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
    public class PersonSeervice : System.Web.Services.WebService
    {
        private int _personId;
        private string _msg;
        private int _result;

        [WebMethod]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
           var  personLogic=new PersonManager();

            _personId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId,userId);
           
            if (_personId > 0)
            {
                _msg = "New Person Added Successfully!";
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
        public string AddPersonRelationship(PersonRelationship _personRelationship)
        {
            try
            {
                var personRelationship=new PersonRelationshipManager();
                _result = personRelationship.AddPersonRelationship(_personId, _personRelationship.RelatedTo,
                    _personRelationship.RelationshipTypeId);
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
        public string AddPersonPopulation(PatientOVCStatus patientOvcStatus)
        {
            try
            {
                var personOvcStatus = new PersonOvcStatusManager();
                _result = personOvcStatus.AddPatientOvcStatus(_personId, patientOvcStatus.GuardianId,
                    patientOvcStatus.Orphan, patientOvcStatus.InSchool);
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
