using System.Collections.Generic;
using Entities.CCC.Triage;

namespace Interface.CCC.Encounter
{
    public interface IPatientAdherenceAssessessment
    {
        int AddPatientAdherenceAssessment(PatientAdherenceAssessment patientAdherenceAssessment);
        int UpdateAdherenceAssessment(PatientAdherenceAssessment patientAdherenceAssessment);
        PatientAdherenceAssessment GetPatientCurrentAdheranceStatus(int patientId);
        List<PatientAdherenceAssessment> GetAdherenceAssessmentsList(int patientId);
        List<PatientAdherenceAssessment> GetActiveAdherenceAssessment(int patientId);
    }
}
