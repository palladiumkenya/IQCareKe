using System.Collections.Generic;
using Application.Presentation;
using Entities.PatientCore;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PersonMaritalStatusManager
    {
        private IPatientMaritalStatusManager _mgr = (IPatientMaritalStatusManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");
        private int result;

        public int AddPatientMaritalStatus(int personId,int maritalStatusId,int userId)
        {
            int maritalStatus;
            var lookUpLogic = new LookupLogic();
            maritalStatus = maritalStatusId > 0 ? maritalStatusId : lookUpLogic.GetItemIdByGroupAndItemName("MaritalStatus", "Child")[0].ItemId;

            PatientMaritalStatus patientMaritalStatus=new PatientMaritalStatus()
            {
                PersonId = personId,
                MaritalStatusId = maritalStatus,
                CreatedBy = userId,
                Active = true  
            };
          return result=  _mgr.AddPatientMaritalStatus(patientMaritalStatus);
        }

       public int UpdatePatientMaritalStatus(PatientMaritalStatus _matStatus)
        {
            return result = _mgr.UpdatePatientMaritalStatus(_matStatus);
        }

        public int DeletePatientMaritalStatus(int id)
        {
          return result=  _mgr.DeletePatientMaritalStatus(id);
        }

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            var myList = _mgr.GetAllMaritalStatuses(personId);
            return myList;
        }

        public PatientMaritalStatus GetInitialPatientMaritalStatus(int personId)
        {
            return _mgr.GetFirstPatientMaritalStatus(personId);
        }
    }
}
