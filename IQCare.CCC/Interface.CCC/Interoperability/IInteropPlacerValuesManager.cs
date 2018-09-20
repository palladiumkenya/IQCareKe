using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Interoperability;

namespace Interface.CCC.Interoperability
{
    public interface IInteropPlacerValuesManager
    {
        InteropPlacerValues GetInteropPlacerValues(int interopPlacerTypeId, int identifierType, string placerValue);

        int AddInteropPlacerValue(InteropPlacerValues interopPlacerValues);
    }
}
