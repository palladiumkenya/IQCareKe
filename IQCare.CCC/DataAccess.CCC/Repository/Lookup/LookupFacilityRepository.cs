using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
namespace DataAccess.CCC.Repository.Lookup
{
   public class LookupFacilityRepository : BaseRepository<LookupFacility>, ILookupFacility,IDisposable
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

        public LookupFacility GetFacility()
        {
            ILookupFacility facilityRepository = new LookupFacilityRepository();
           
            var deleteFlag = 0;
            var facility = facilityRepository.FindBy(x => x.DeleteFlag == deleteFlag).FirstOrDefault();
            //var list = myList.GroupBy(x => x.FacilityID).Select(x => x.First()).OrderBy(x => x.FacilityName);
            return facility;
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




    }
}
