using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientCategorizationRepository : IRepository<PatientCategorization>
    {
        List<PatientCategorization> GetByPatientId(int patientId);
    }
}