using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Tb;

namespace DataAccess.CCC.Interface.Tb
{
    public interface IPatientIptOutcomeRepository : IRepository<PatientIptOutcome>
    {
        List<PatientIptOutcome> GetByPatientId(int patientId);
    }
}