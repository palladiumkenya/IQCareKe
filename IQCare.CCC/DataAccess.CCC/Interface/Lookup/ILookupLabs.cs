using System;
using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupLabs : IRepository<LookupLabs>     
    {
        List<LookupLabs> GetLabs();
        LookupLabs GetLabTestId(string labType);
        List<LookupLabs> FindBy(Func<LookupLabs, bool> p);
    }
}
