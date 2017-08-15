using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupRepository :BaseRepository<LookupItemView>,ILookupRepository
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
            var results = _context.Lookups.Where(p).ToList();
            //  .Where(p).ToList<LookupCounty>();

            return results;//.ToList();
        }

        public List<LookupItemView> GetLookupItemViews(string listGroup)
        {
            //ILookupRepository x = new LookupRepository();
            var myList = _context.Lookups.Where(g => g.MasterName == listGroup).OrderBy(y => y.OrdRank).ToList();
            
                //FindBy(g => g.MasterName == listGroup.ToString()).OrderBy(y=>y.OrdRank).ToList();
           // return myList;//.OrderBy(l => l.OrdRank).ToList();
           return myList;
        }
        /* pw GetLookupLabs implementation   */
        public List<LookupItemView> GetLabsList(string lab)
        {
            ILookupRepository x = new LookupRepository();
            var myList = x.FindBy(g => g.MasterName == lab.ToString()).OrderBy(l => l.OrdRank).ToList();
            return myList;//.OrderBy(l => l.OrdRank).ToList();
            //  return myList;
        }
        public LookupItemView GetPatientGender(int genderId)
        {
            ILookupRepository lookupGender = new LookupRepository();
           var genderType = lookupGender.GetById(genderId);
            //var genderType = lookupGender.FindBy(x => x.ItemId == genderId).FirstOrDefault();
            return genderType;

        }
    }
}
