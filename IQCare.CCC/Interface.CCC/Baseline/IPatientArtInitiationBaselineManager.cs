using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientArtInitiationBaselineManager
    {
        int AddArtInitiationBaseline(PatientArtInitiationBaseline patientArtInitiationBaseline );
        int UpdateArtInitiationBaseline(PatientArtInitiationBaseline patientArtInitiationBaseline);
        int DeleteArtInitiationBaseline(int id);
        List<PatientArtInitiationBaseline> GetPatientArtInitiationBaseline(int patientId);
    }
}
