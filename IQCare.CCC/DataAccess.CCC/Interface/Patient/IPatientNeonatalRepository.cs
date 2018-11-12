using DataAccess.Context;
using Entities.CCC.Neonatal;
using System.Collections.Generic;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientNeonatalRepository : IRepository<PatientMilestone>
    {
        //PatientNeonatal GetByPatientId(int patientId);
    }
    public interface IImmunizationHistoryRepository:IRepository<PatientImmunizationHistory>
    {
        List<PatientImmunizationHistory> getPatientImmunization(int patientId);
    }
}