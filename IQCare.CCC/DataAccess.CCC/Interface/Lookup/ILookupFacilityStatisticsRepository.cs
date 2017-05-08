using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupFacilityStatisticsRepository :IRepository<LookupFacilityStatistics>
   {
      List<LookupFacilityStatistics> GetFacilityStatistics();
   }
}
