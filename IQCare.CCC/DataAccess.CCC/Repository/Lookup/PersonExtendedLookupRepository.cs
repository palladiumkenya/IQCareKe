using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PersonExtendedLookupRepository: BaseRepository<PersonExtLookup> ,IPersonExtendedLookupRepository
    {
        private readonly LookupContext _context;

        public PersonExtendedLookupRepository() : this(new LookupContext())
        {

        }

        public PersonExtendedLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}