using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Collections.Generic;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientFamilyTestingRepository : IRepository<PatientFamilyTesting>
    {
        List<PatientFamilyTesting> GetByPatientId(int patientId);
    }
}