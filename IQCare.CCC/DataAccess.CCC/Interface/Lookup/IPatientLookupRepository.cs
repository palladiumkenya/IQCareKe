using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface IPatientLookupRepository : IRepository<PatientLookup>
    {
        PatientLookup GetGender(int patientId);
    }
}
