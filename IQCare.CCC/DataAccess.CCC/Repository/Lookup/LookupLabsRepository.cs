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
   public class LookupLabsRepository : BaseRepository<LookupLabs>, ILookupLabs,IDisposable
    {
        private readonly LookupContext _context;

        public LookupLabsRepository() : this(new LookupContext())
        {
        }

        public LookupLabsRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupLabs> FindBy(Func<LookupLabs, bool> p)
        {
            var results = _context.LookupLaboratories.Where(p);
            //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }

        public List<LookupLabs> GetLabs()
        {
            ILookupLabs labsRepository = new LookupLabsRepository();
            var list = labsRepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.ParameterName);
            return list.ToList();
        }

        public LookupLabs GetLabTestId(string labType)
        {
            ILookupLabs labsRepository = new LookupLabsRepository();
            var labTestId = labsRepository.FindBy(x => x.ParameterName == labType).FirstOrDefault();
            return labTestId;

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
