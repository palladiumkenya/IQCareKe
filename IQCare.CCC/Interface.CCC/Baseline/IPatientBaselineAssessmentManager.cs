using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientBaselineAssessmentManager
    {
        int AddPatientBaselineAssessment(PatientBaselineAssessment patientArtInitiationBaseline );
        int UpdatePatientBaselineAssessment(PatientBaselineAssessment patientArtInitiationBaseline);
        int DeletePatientBaselineAssessment(int id);
        List<PatientBaselineAssessment> GetPatientBaselineAssessment(int patientId);
        int CheckIfPatientBaselineExists(int patientId);
    }
}
