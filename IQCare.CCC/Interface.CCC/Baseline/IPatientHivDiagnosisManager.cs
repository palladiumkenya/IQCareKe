using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientHivDiagnosisManager
    {
        int AddPatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis);
        int UpdatePatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis);
        int DeletePatientHivDiagnosis(int id);
        List<PatientHivDiagnosis> GetPatientHivDiagnosis(int patientId);
        int CheckIfDiagnosisExists(int patientId);
    }
}