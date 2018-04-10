using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
namespace DataAccess.CCC.Repository.Lookup
{
   public class LookupFacilityRepository : BaseRepository<LookupFacility>, ILookupFacility
    {
        private readonly LookupContext _context;

        public LookupFacilityRepository() : this(new LookupContext())
        {
        }

        public LookupFacilityRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupFacility> FindBy(Func<LookupFacility, bool> p)
        {
            var results = _context.LookupFacility.Where(p);
            //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }
        public LookupFacility GetFacilityByMflCode(string mflCode)
        {
            ILookupFacility facilityRepository = new LookupFacilityRepository();

            var deleteFlag = 0;
            var facility = _context.LookupFacility.Where(x => x.DeleteFlag == deleteFlag && mflCode== x.MFLCode).FirstOrDefault();
            //var list = myList.GroupBy(x => x.FacilityID).Select(x => x.First()).OrderBy(x => x.FacilityName);
            return facility;
        }
        public LookupFacility GetFacility()
        {
            ILookupFacility facilityRepository = new LookupFacilityRepository();
           
            var deleteFlag = 0;
            var facility = facilityRepository.FindBy(x => x.DeleteFlag == deleteFlag).FirstOrDefault();
            //var list = myList.GroupBy(x => x.FacilityID).Select(x => x.First()).OrderBy(x => x.FacilityName);
            return facility;
        }

    }
}
