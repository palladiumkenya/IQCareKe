using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Tb;

namespace DataAccess.CCC.Interface.Tb
{
    public interface IPatientIptRepository : IRepository<PatientIpt>
    {
        List<PatientIpt> GetByPatientId(int patientId);
    }
}