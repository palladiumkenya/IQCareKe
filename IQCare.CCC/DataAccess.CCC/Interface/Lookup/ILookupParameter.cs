using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupParameter
    {
        List<LookupTestParameter>GetTestParameter(int LabTestId);
       List<LookupTestParameter> FindBy(Func<LookupTestParameter, bool> p);
    }
}
