using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface IPatientTreatmentTrackerLookupRepository:IRepository<PatientTreamentTrackerLookup>
    {
        PatientTreamentTrackerLookup GetPatientbaselineRegimenLookup(int patientId);
        PatientTreamentTrackerLookup GetCurrentPatientRegimen(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentSwitchesList(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentInterrupList(int patientId);
        List<PatientTreamentTrackerLookup> GetPatientTreatmentSubstitutionList(int patientId);
        
    }
}
