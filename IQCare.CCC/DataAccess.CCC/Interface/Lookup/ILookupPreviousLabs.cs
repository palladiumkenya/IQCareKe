using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Lookup;
namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupPreviousLabs : IRepository<LookupPreviousLabs>
    {
        List<LookupPreviousLabs> GetPreviousLabs(int patientId);
        List<LookupPreviousLabs> GetVlLabs(int patientId);
        // List<LookupPreviousLabs> FindBy(Func<LookupPreviousLabs, bool> p);
    }
}
