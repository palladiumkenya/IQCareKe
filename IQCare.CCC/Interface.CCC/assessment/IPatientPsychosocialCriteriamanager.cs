using Entities.CCC.Assessment;
using System.Collections.Generic;

namespace Interface.CCC.assessment
{
    public interface PatientPsychosicialCriteriaManager
    {

        int checkIfARTPreparationExists(int patientId);
        int AddPreparation(PatientPsychoscialCriteria p);
        int EditPreparation(PatientPsychoscialCriteria p);
        int DeletePreparation(int Id);
        List<PatientPsychoscialCriteria> GetPatientPsychosocialCriteriaDetails(int patientId);
    }
}
