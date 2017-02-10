using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientArtUseHistoryManager
    {
        int AddPatientArtUseHistory(PatientArtUseHistory patientArtUseHistory);
        int UpdatePatientArtUseHistory(PatientArtUseHistory patientArtUseHistory);
        int DeletePatientArtUseHistory(int id);
        List<PatientArtUseHistory> GetPatientArtUseHistory(int patientId);
    }
}
