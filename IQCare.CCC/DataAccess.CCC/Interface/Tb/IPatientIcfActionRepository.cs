using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Tb;

namespace DataAccess.CCC.Interface.Tb
{
    public interface IPatientIcfActionRepository : IRepository<PatientIcfAction>
    {
        List<PatientIcfAction> GetByPatientId(int patientId);
    }
}