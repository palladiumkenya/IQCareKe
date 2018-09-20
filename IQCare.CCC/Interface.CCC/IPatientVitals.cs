using System.Collections.Generic;
using Entities.CCC.Triage;
namespace Interface.CCC
{
    public interface IPatientVitals
    {
        int AddPatientVitals(PatientVital p);
        PatientVital GetPatientVitals(int id);
        void DeletePatientVitals(int id);
        int UpdatePatientVitals(PatientVital p);
        PatientVital GetByPatientId(int patientId);
        PatientVital GetByPatientVisitId(int patientVisitId);
        List<PatientVital> GetCurrentPatientVital(int patientId);
        PatientVital GetPatientVitalsByMasterVisitId(int patientId, int patientMasterVisitId);
        PatientVital GetPatientVitalsBaseline(int patientId);
        List<PatientVital> GetAllPatientVitals(int patientId);
    }
}