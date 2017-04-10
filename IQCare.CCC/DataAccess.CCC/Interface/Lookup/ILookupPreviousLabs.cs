using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;
namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupPreviousLabs : IRepository<LookupPreviousLabs>
    {
        List<LookupPreviousLabs> GetPreviousLabs(int patientId);
        List<LookupPreviousLabs> GetVlLabs(int patientId);
        List<LookupPreviousLabs> GetPendingVlLabs(int patientId);
        List<LookupPreviousLabs> GetPendingLabs(int patientId);
        // List<LookupPreviousLabs> FindBy(Func<LookupPreviousLabs, bool> p);
    }
}
