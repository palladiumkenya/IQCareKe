using Entities.CCC.Triage;

namespace Interface.CCC.Triage
{
    public interface IPatientAdverseEventOutcomeManager
    {
        int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId);
        int SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome);
        int UpdatePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome);
        int DeletePatientAdverseEventOutcome(int id);
    }
}
