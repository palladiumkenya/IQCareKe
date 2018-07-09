using System.Collections.Generic;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientOIManager
    {
        PatientOI addPatientOI(PatientOI patientoi);
        List<PatientOI> GetPatientOIs(int patientId, int patientMasterVisitId);
        PatientOI GetPatientOIbyId(int entityId);
        PatientOI UpdatePatientOI(PatientOI patientOI);
        PatientOI GetPatientOI(int patientId, int patientMasterVisitId, int OI);
    }
}