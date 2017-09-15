using System.Collections.Generic;
using Entities.CCC.Lookup;

namespace Interface.CCC.Lookup
{
    public interface IPatientTreatmentTrackerManager
    {
        PatientTreamentTrackerLookup GetPatientBaselineRegimenLookup(int patientId);
        PatientTreamentTrackerLookup GetCurrentPatientRegimen(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentSwitchesList(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentInterrupList(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentSubstitutionList(int patientId);
        bool HasPatientTreatmentStarted(int patientId);
    }
}
