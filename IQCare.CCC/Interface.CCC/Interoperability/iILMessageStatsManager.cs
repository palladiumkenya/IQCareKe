using System.Collections.Generic;
using Entities.CCC.IL;

namespace Interface.CCC.Interoperability
{
    public interface iILMessageStatsManager
    {
        List<ILMessageStats> GetIlMessageStats();
    }
}