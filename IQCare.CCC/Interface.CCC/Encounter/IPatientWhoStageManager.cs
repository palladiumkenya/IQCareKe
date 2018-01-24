using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientWhoStageManager
    {
        int addPatientWhoStage(PatientWhoStage patientWhoStage);
        PatientWhoStage GetPatientWhoStage(int patientId, int patientMasterVisitId);
        int UpdatePatientWhoStage(PatientWhoStage patientWhoStage);
        PatientWhoStage GetWhoStageById(int entityId);
    }
}
