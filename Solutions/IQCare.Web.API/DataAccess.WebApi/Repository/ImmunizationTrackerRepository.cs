using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class ImmunizationTrackerRepository : BaseRepository<ImmunizationTracker>,IImmunizationTrackerRepository
    {
        private readonly PsmartContext _context;
        public ImmunizationTrackerRepository()
        {
        }

        public ImmunizationTrackerRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}