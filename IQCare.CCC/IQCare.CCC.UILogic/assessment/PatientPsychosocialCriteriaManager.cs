using Application.Presentation;
using Entities.CCC.Assessment;
using Interface.CCC.assessment;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.assessment
{
    public class PatientPsychosocialCriteriaManager
    {
        private PatientPsychosicialCriteriaManager _PatientPsychosicialCriteriaManager = (PatientPsychosicialCriteriaManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.assessment.BPatientPsychosocialCriteriaManager, BusinessProcess.CCC");
        private int result;

        public int CheckIfARTPreparationExists(int patientId)
        {
            result = _PatientPsychosicialCriteriaManager.checkIfARTPreparationExists(patientId);
            return result;
        }
        public int AddPreparation(PatientPsychoscialCriteria _patientPsychosocialCriteria)
        {

            result = _PatientPsychosicialCriteriaManager.AddPreparation(_patientPsychosocialCriteria);
            return result;
        }
        public int EditPreparation(PatientPsychoscialCriteria _patientPsychosocialCriteria)
        {
            result = _PatientPsychosicialCriteriaManager.EditPreparation(_patientPsychosocialCriteria);
            return result;
        }
        public int DeletePreparation(int Id)
        {
            result = _PatientPsychosicialCriteriaManager.DeletePreparation(Id);
            return result;
        }

        public List<PatientPsychoscialCriteria> GetPatientPsychosocialCriteriaDetails(int patientId)
        {
            return _PatientPsychosicialCriteriaManager.GetPatientPsychosocialCriteriaDetails(patientId);
        }

    }
}
