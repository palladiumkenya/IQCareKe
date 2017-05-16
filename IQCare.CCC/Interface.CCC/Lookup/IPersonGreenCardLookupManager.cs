using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Lookup
{
    public interface IPersonGreenCardLookupManager
    {
        List<PersonGreenCardLookup> GetPtnPkByPersonId(int personId);
    }
}
