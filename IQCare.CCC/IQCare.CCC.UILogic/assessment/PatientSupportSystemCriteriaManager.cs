using System.Collections.Generic;
using Entities.CCC.Assessment;
using Application.Presentation;

namespace IQCare.CCC.UILogic.assessment
{
    public class PatientSupportSystemCriteriaManager 
    {
        private PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = (PatientSupportSystemCriteriaManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientSupportSystemCriteriaManager, BusinessProcess.CCC");
      

        public int AddPreparation(PatientSupportSystemCriteria _PatientSupportSystemCriteria)
        {
            return _PatientSupportSystemCriteriaManager.AddPreparation(_PatientSupportSystemCriteria);
        }

        public int checkIfARTPreparationExists(int patientId)
        {
            return _PatientSupportSystemCriteriaManager.checkIfARTPreparationExists(patientId);
        }

        public int DeletePreparation(int Id)
        {
            return _PatientSupportSystemCriteriaManager.DeletePreparation(Id);
        }

        public int EditPreparation(PatientSupportSystemCriteria _PatientSupportSystemCriteria)
        {
            return _PatientSupportSystemCriteriaManager.EditPreparation(_PatientSupportSystemCriteria);
        }

        public List<PatientSupportSystemCriteria> GetPatientSupportSystemCriteriaDetails(int patientId)
        {
            return _PatientSupportSystemCriteriaManager.GetPatientSupportSystemCriteriaDetails(patientId);
        }
    }
}
