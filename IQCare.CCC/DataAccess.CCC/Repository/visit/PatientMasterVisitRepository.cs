using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
    public class PatientMasterVisitRepository : BaseRepository<PatientMasterVisit>, IPatientMasterVisitRepository
    {
        private readonly GreencardContext _context;

        public PatientMasterVisitRepository() : this(new GreencardContext())
        {

        }

        public PatientMasterVisitRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
