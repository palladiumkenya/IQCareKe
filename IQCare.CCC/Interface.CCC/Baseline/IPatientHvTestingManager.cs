using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientHvTestingManager
    {
        int AddPatientHivTesting(PatientHivTesting patientHivTesting);
        int UpdatePatientHivTesting(PatientHivTesting patientHivTesting);
        int DeletePatientHivTesting(int id);
        List<PatientHivTesting> GetPatientHivTestings(int patientId);
    }
}
