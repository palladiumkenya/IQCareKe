using Application.Presentation;
using Entities.Records;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
   public  class PersonEmergencyContactManager
    {
        private IPersonEmergencyContactManager _mgr = (IPersonEmergencyContactManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonEmergencyContactManager, BusinessProcess.Records");
         private int _result;
        private string _msg;
        public int AddPersonEmergencyContact(int personId, int emergencyContactPersonId,string mobileContact,int userId)
        {
            PersonEmergencyContact pm = new PersonEmergencyContact()
            {
                PersonId = personId,
                EmergencyContactPersonId = emergencyContactPersonId,
                MobileContact = mobileContact,
                CreatedBy = userId

            };
            _result = _mgr.AddPersonEmergencyContact(pm);
            return _result;
        }

       public string  DeletePersonEmergencyContact(int id)
        {
            try
            {
                _result = _mgr.DeletePersonEmergencyContact(id);
                if(_result >0)
                {
                    _msg = "Person EmergencyContact deleted successfully";
                }
            }
            catch(Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
            }
            return _msg;
        }

        public  List<PersonEmergencyContact> GetPatientEmergencyContact(int personId)
        {
            List<PersonEmergencyContact> myList;
            try
            {
                myList = _mgr.GetCurrentEmergencyContact(personId);
             
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
                throw;
            }
            return myList;
        }

        public List<PersonEmergencyContact> GetAllPersonEmergencyContact(int personId)
        {
            List<PersonEmergencyContact> myList;
            try
            {
                myList = _mgr.GetAllEmergencyContact(personId);

            }
            catch(Exception e)
            {
                _msg=e.Message+ ' ' + e.InnerException;
                throw;
            }
            return myList;
        }
        public int UpdatePersonEmergencyContact(PersonEmergencyContact supporter)
        {
            _result = _mgr.UpdatePersonEmergencyContact(supporter);
            return _result;
        }
    }
}
