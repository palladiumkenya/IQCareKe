using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.Lookup
{
    public interface  ILookupICDCodesManager
    {
        List<ICDCodeList> GetICDCodeList();
    }
}
