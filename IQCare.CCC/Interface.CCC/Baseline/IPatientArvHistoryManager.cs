using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientArvHistoryManager
    {
        int AddPatientArvHistory(PatientArvHistory patientArtUseHistory);
        int UpdatePatientArvHistory(PatientArvHistory patientArtUseHistory);
        int DeletePatientArvHistory(int id);
        List<PatientArvHistory> GetPatientArvHistory(int patientId);
    }
}
