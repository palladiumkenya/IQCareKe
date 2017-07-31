using System.Collections.Generic;
using Entities.CCC.Assessment;
using Application.Presentation;
using Interface.CCC.assessment;

namespace IQCare.CCC.UILogic.assessment
{
    public class PatientSupportSystemCriteriaManager 
    {
        private IPatientSupportSystemsCriteriaManager _PatientSupportSystemCriteriaManager = (IPatientSupportSystemsCriteriaManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.assessment.BPatientSupportSystemCriteriaManager, BusinessProcess.CCC");
      

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
