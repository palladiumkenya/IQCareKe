using Entities.CCC.Triage;

namespace Interface.CCC.Encounter
{
    public interface IPatientWhoStageManager
    {
        int addPatientWhoStage(PatientWhoStage patientWhoStage);
        PatientWhoStage GetPatientWhoStage(int patientId, int patientMasterVisitId);
        int UpdatePatientWhoStage(PatientWhoStage patientWhoStage);
    }
}
