using Entities.CCC.Encounter;
using System.Collections.Generic;

namespace Interface.CCC
{
    public interface IPatientFamilyTesting
    {
        int AddPatientFamilyTestings(PatientFamilyTesting p);

        PatientFamilyTesting GetPatientFamilyTestings(int id);

        void DeletePatientFamilyTestings(int id);

        int UpdatePatientFamilyTestings(PatientFamilyTesting p);

        List<PatientFamilyTesting> GetByPatientId(int patientId);
    }
}