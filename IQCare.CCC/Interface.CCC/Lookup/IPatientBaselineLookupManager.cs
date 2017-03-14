using Entities.CCC.Lookup;
using System.Collections.Generic;

namespace Interface.CCC.Lookup
{
    public interface IPatientBaselineLookupManager
    {
        List<PatientBaselineLookup> GetAllPatientBaseline(int patientId);
        List<PatientBaselineLookup> GetPatientTransferinStatus(int patientId);
        List<PatientBaselineLookup> GetPatientHIVDiagnosis(int patientId);
        List<PatientBaselineLookup> GetPatientBaselineAssessment(int patientId);
        List<PatientBaselineLookup> GetPatientTreatmentInitiation(int patientId);
    }
}
