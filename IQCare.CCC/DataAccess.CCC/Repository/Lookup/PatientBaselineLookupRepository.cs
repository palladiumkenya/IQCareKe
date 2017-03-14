using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientBaselineLookupRepository:BaseRepository<PatientBaselineLookup>,IPatientBaselineLookupRepository
    {
        private readonly LookupContext _context;

        public PatientBaselineLookupRepository():this(new LookupContext())
        {

        }

        public PatientBaselineLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
