using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using DataAccess.CCC.Context;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupFacilityStatisticsRepository : BaseRepository<LookupFacilityStatistics>, ILookupFacilityStatisticsRepository
    {

        private readonly LookupContext _context;

        public LookupFacilityStatisticsRepository() : this(new LookupContext())
        {
        }

        public LookupFacilityStatisticsRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupFacilityStatistics>  GetFacilityStatistics()
        {
            ILookupFacilityStatisticsRepository lookupFacilityStatistics=new LookupFacilityStatisticsRepository();
            return lookupFacilityStatistics.GetAll().ToList();
        }
    }
}
