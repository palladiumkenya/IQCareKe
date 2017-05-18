using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System.Collections.Generic;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupFacilityViralLoadRepository : BaseRepository<LookupFacilityViralLoad>, ILookupFacilityViralLoad
    {
        private readonly LookupContext _context;

        public LookupFacilityViralLoadRepository():this(new LookupContext())
        {

        }

        public LookupFacilityViralLoadRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
        public List<LookupFacilityViralLoad> GetFacilityVLUnSuppressed(int facilityId)
        {

            ILookupFacilityViralLoad unsuppressedVL = new LookupFacilityViralLoadRepository();
            var myList = unsuppressedVL.FindBy(p => p.FacilityId == facilityId &
                                                    p.ResultValues >= 1000)                                                 
                                                   .ToList();


          return myList;
        }

        public List<LookupFacilityViralLoad> GetFacilityVLSuppressed(int facilityId)
        {

            ILookupFacilityViralLoad suppressedVL = new LookupFacilityViralLoadRepository();
            var myList = suppressedVL.FindBy(p => p.FacilityId == facilityId &
                                                    p.ResultValues < 1000)
                                                   .ToList();


            return myList;
        }

    }
}
