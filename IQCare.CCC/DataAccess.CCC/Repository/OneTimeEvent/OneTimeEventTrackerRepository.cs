using DataAccess.CCC.Context;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.OneTimeEvent
{
    public class OneTimeEventTrackerRepository: BaseRepository<PatientDisclosure>
    {
        private readonly GreencardContext _context;

        public OneTimeEventTrackerRepository(): base (new GreencardContext())
        {

        }

        public OneTimeEventTrackerRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
