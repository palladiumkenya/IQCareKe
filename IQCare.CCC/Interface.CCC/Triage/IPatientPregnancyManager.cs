using Entities.CCC.Triage;
using System.Collections.Generic;

namespace Interface.CCC.Triage
{
    public interface IPatientPregnancyManager
    {
        int AddPatientPregnancy(PatientPreganancy a);
        int UpdatePatientPreganacy(PatientPreganancy u);
        int DeletePatientPregnancy(int Id);
        List<PatientPreganancy> GetPatientPregnancy(int patientId);
        int CheckIfPatientPregnancyExisists(int patientId);
    }
}
