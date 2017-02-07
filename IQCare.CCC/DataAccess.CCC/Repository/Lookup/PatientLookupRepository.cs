using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientLookupRepository:BaseRepository<PatientLookup>,IPatientLookupRepository
    {
        private readonly LookupContext _context;

        public PatientLookupRepository():this(new LookupContext())
        {
            
        }

        public PatientLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
