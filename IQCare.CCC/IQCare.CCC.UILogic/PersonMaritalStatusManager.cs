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
            PatientMaritalStatus patientMaritalStatus=new PatientMaritalStatus()
            {
                PersonId = personId,
                MaritalStatusId = maritalStatusId,
                CreatedBy = userId,
                Active = true  
            };
          return result=  _mgr.AddPatientMaritalStatus(patientMaritalStatus);
        }

       public int UpdatePatientMaritalStatus(int maritalstatusId)
        {
            PatientMaritalStatus patientMaritalStatus=new PatientMaritalStatus()
            {
                MaritalStatusId = maritalstatusId
            };

           return result= _mgr.UpdatePatientMaritalStatus(patientMaritalStatus);
        }

        public int DeletePatientMaritalStatus(int id)
        {
          return result=  _mgr.DeletePatientMaritalStatus(id);
        }

        List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            var myList = _mgr.GetAllMaritalStatuses(personId);
            return myList;
        }
    }
}
