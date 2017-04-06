using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
   public class PatientLabTrackerRepository : BaseRepository<PatientLabTracker>, IPatientLabTrackerRepository
    {
        private readonly GreencardContext _context;

        public PatientLabTrackerRepository():this(new GreencardContext())
       {

        }

        public PatientLabTrackerRepository(GreencardContext context) : base(context)
       {
            _context = context;
        }

    }
}
