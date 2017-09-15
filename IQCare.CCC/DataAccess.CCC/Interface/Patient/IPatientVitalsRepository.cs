using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientVitalsRepository : IRepository<PatientVital>
    {
        PatientVital GetByPatientId(int patientId);
    }
}