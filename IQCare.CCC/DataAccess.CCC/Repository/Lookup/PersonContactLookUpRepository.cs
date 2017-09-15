using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PersonContactLookUpRepository:BaseRepository<PersonContactLookUp>,IPersonContactLookUpRepository
    {
        private readonly LookupContext _context;

        public PersonContactLookUpRepository():this(new LookupContext())
        {
            
        }

        public PersonContactLookUpRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

    }
}
