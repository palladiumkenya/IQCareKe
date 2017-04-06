using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupCountyRepository:BaseRepository<LookupCounty>, ILookupCounty
    {
        private readonly LookupContext _context;

        public LookupCountyRepository() : this(new LookupContext())
        {
        }

       public LookupCountyRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupCounty> FindBy(Func<LookupCounty, bool> p)
        {
           var results = _context.LookupCounties.Where(p);
              //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }

        public List<LookupCounty> GetCounties()
        {
            ILookupCounty countyRepository = new LookupCountyRepository();
            var list = countyRepository.GetAll().GroupBy(x => x.CountyId).Select(x => x.First()).OrderBy(l=>l.CountyName);
            return list.ToList();
        }

        public List<LookupCounty> GetSubCounties(string county)
        {
           ILookupCounty lookupCountyRepository = new LookupCountyRepository();
            var myList = lookupCountyRepository.FindBy(s => s.CountyName == county);
            var list = myList.GroupBy(x => x.SubcountyId).Select(x => x.First()).OrderBy(x => x.SubcountyName);
            return list.ToList();
        }

        public List<LookupCounty> GetWardsList(string subcounty)
        {
            ILookupCounty lookupCountyRepository =new LookupCountyRepository();
            var myList =lookupCountyRepository.FindBy(x=>x.SubcountyName==subcounty);
            var list = myList.GroupBy(x => x.WardId).Select(x => x.First()).OrderBy(x => x.WardName);
            return list.Distinct().ToList();
        }

    }

}
