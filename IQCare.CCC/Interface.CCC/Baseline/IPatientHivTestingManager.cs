using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientHivTestingManager
    {
        int AddPatientHivTesting(PatientHivTesting patientHivTesting);
        int UpdatePatientHivTesting(PatientHivTesting patientHivTesting);
        int DeletePatientHivTesting(int id);
        List<PatientHivTesting> GetPatientHivTestings(int patientId);
    }
}
