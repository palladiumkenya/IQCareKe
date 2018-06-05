using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Triage;

namespace Interface.CCC.Triage
{
    public interface IPregnancyOutcomeLookupManager
    {
        PregnancyOutcomeLookup GetLastPregnancyOutcomeLookup(int patientId);
    }
}
