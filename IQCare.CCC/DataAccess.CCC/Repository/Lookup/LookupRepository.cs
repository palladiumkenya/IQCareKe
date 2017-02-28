using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupRepository :BaseRepository<Entities.CCC.Lookup.LookupItemView>,ILookupRepository
    {
        private readonly LookupContext _context;

        public LookupRepository() : this(new LookupContext())
        {
        }

        public LookupRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupItemView> FindBy(Func<LookupItemView, bool> p)
        {
            var results = _context.Lookups.Where(p);
            //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }

        public List<LookupItemView> GetLookupItemViews(string listGroup)
        {
            ILookupRepository x = new LookupRepository();
            var myList = x.FindBy(g => g.MasterName == listGroup.ToString());
           return myList.OrderBy(l => l.OrdRank).ToList();
          //  return myList;
        }
        /* pw GetLookupLabs implementation   */
        public List<LookupItemView> GetLabsList(string lab)
        {
            ILookupRepository x = new LookupRepository();
            var myList = x.FindBy(g => g.MasterName == lab.ToString());
            return myList.OrderBy(l => l.OrdRank).ToList();
            //  return myList;
        }
        public LookupItemView GetPatientGender(int genderID)
        {
            ILookupRepository lookupGender = new LookupRepository();
            var genderType = lookupGender.FindBy(x => x.ItemId == genderID).FirstOrDefault();
            return genderType;

        }

    }
}
