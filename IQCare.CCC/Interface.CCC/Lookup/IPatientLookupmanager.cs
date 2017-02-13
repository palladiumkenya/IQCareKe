using System.Collections.Generic;
using Entities.CCC.Lookup;

namespace Interface.CCC.Lookup
{
    public interface IPatientLookupmanager
    {
        List<PatientLookup> GetPatientSearchPayload();
        List<PatientLookup> GetPatientDetailsLookup(int id);
        int GetTotalpatientCount();
    }
}
 