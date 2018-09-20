using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.IL;
using DataAccess.Context;
using Entities.CCC.IL;

namespace DataAccess.CCC.Repository.IL
{
    public class IlStatisticsRepository :BaseRepository<IlStatistics>,IIlStatisticsRepository
    {
        private readonly LookupContext _context;

        public IlStatisticsRepository(LookupContext context):base(context)
        {
            _context = context;
        }

        protected IlStatisticsRepository():this(new LookupContext())
        {

        }
    }
}