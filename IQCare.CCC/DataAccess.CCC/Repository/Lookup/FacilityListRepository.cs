using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class FacilityListRepository : BaseRepository<FacilityList>, IFacilityListRepository
    {
        private readonly LookupContext _context;

        public FacilityListRepository() : this(new LookupContext())
        {
            
        }

        public FacilityListRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
