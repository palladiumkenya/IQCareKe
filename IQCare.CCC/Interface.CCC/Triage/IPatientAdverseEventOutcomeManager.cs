using System.Collections.Generic;
using Entities.CCC.Triage;

namespace Interface.CCC.Triage
{
    public interface IPatientAdverseEventOutcomeManager
    {
        int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId,int patientMasterVisitId);
        int SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome);
        int UpdatePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome);
        int DeletePatientAdverseEventOutcome(int id);
        List<PatientAdverseEventOutcome> GetAdverseEventOutcome(int adverseId,int patientId);
        //string GetCurrentAdverseEventOutcome(int adverseId, int patientMasterVisitId, int patientId);
        //string GetCurrentAdverseEventOutcomeDate(int adverseId, int patientMasterVisitId, int patientId);

    }
}
