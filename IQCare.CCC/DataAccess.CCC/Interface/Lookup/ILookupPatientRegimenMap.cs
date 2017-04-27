using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface  ILookupPatientRegimenMap:IRepository<PatientRegimenLookup>
    {
        PatientRegimenLookup GetPatientCurrentRegimen(int patientId);
        List<PatientRegimenLookup> GetPatientRegimenList(int patientId);
    }
}
