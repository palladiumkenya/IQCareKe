using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class TestingSummaryStatisticsRepository:BaseRepository<TestingSummaryStatistics>,ITestingSummaryStatisticsRepository
    {
        private readonly LookupContext _context;

        public TestingSummaryStatisticsRepository() : this(new LookupContext())
        {

        }

        public TestingSummaryStatisticsRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
