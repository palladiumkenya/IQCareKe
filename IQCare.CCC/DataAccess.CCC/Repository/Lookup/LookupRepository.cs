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
    public class LookupRepository :BaseRepository<Entities.CCC.Lookup.LookupItemView>,ILookupRepository,IDisposable
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
        public LookupItemView GetPatientGender(int genderId)
        {
            ILookupRepository lookupGender = new LookupRepository();
            var genderType = lookupGender.FindBy(x => x.ItemId == genderId).FirstOrDefault();
            return genderType;

        }

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
            GC.SuppressFinalize(this);
        }

        ~LookupRepository()
        {
            Dispose(false);
        }
    }
}
