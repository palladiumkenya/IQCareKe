using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupLabs : IRepository<LookupLabs>     
    {
        List<LookupLabs> GetLabs();
        List<LookupLabs> FindBy(Func<LookupLabs, bool> p);
    }
}
