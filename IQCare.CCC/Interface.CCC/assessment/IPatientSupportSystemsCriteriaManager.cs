using Entities.CCC.Assessment;
using System.Collections.Generic;

namespace Interface.CCC.assessment
{
    public interface IPatientSupportSystemsCriteriaManager
    {
        int checkIfARTPreparationExists(int patientId);
        int AddPreparation(PatientSupportSystemCriteria p);
        int EditPreparation(PatientSupportSystemCriteria p);
        int DeletePreparation(int Id);
        List<PatientSupportSystemCriteria> GetPatientSupportSystemCriteriaDetails(int patientId);
    }
}
