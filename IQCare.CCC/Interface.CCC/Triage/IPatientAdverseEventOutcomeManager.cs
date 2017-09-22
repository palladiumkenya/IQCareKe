using Entities.CCC.Triage;

namespace Interface.CCC.Triage
{
    public interface IPatientAdverseEventOutcomeManager
    {
        int GetPatientAdverseEventOutcomeStatus(int id, int patientId);
        int AddPatientAdverseEventOutcome(PatientAdverseEventsOutcome patientAdverseEventsOutcome);
        int EditPatientAdverseEventOutcome(PatientAdverseEventsOutcome patientAdverseEvent);
        int DeletePatientAdverseEventOutcome(int id);
    }
}
