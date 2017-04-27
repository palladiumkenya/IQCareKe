using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupPatientAdherenceRepository :IRepository<LookupPatientAdherence>
    {
        LookupPatientAdherence GetPatientAdherenceStatus(int patientId);

    }
}
