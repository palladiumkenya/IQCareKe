using System.Collections.Generic;
using Application.Presentation;
using Entities.PatientCore;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PersonMaritalStatusManager
    {
        private IPatientMaritalStatusManager _mgr = (IPatientMaritalStatusManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");

        public void AddPatientMaritalStatus(int patientMasteVisitId,int personId,int maritalStatusId)
        {
            PatientMaritalStatus patientMaritalStatus=new PatientMaritalStatus()
            {
                PatientMasterVisitId = patientMasteVisitId,
                PersonId = personId,
                MaritalStatusId = maritalStatusId,
                Active = true  
            };
            _mgr.AddPatientMaritalStatus(patientMaritalStatus);
        }

        void UpdatePatientMaritalStatus(int maritalstatusId)
        {
            PatientMaritalStatus patientMaritalStatus=new PatientMaritalStatus()
            {
                MaritalStatusId = maritalstatusId
            };

            _mgr.UpdatePatientMaritalStatus(patientMaritalStatus);
        }

        void DeletePatientMaritalStatus(int id)
        {
            _mgr.DeletePatientMaritalStatus(id);
        }

        List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            var myList = _mgr.GetAllMaritalStatuses(personId);
            return myList;
        }
    }
}
