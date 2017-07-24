using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientCategorizationRepository : BaseRepository<PatientCategorization>, IPatientCategorizationRepository
    {
        private GreencardContext _context;

        public PatientCategorizationRepository() : this(new GreencardContext())
        {
        }

        public PatientCategorizationRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}