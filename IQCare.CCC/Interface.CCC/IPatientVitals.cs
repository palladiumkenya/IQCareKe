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
    }
}