using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Tb;

namespace DataAccess.CCC.Interface.Tb
{
    public interface IPatientIcfRepository : IRepository<PatientIcf>
    {
        List<PatientIcf> GetByPatientId(int patientId);
    }
}