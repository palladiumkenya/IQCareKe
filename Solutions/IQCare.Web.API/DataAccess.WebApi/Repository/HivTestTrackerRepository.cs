using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class HivTestTrackerRepository:BaseRepository<HivTestTracker>,IHivTestTrackerRepository
    {
        private readonly PsmartContext _context;
        public HivTestTrackerRepository():this(new PsmartContext())
        {
        }

        public HivTestTrackerRepository(PsmartContext context):base(context)
        {
            _context = context;
        }
    }
}