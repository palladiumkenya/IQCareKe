using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
    public class PatientMasterVisitRepository : BaseRepository<PatientMasterVisit>, IPatientMasterVisitRepository,IDisposable
    {
        private  GreencardContext _context;

        public PatientMasterVisitRepository() : this(new GreencardContext())
        {

        }

        public PatientMasterVisitRepository(GreencardContext context) : base(context)
        {
            _context = context;
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

        ~PatientMasterVisitRepository()
        {
            this.Dispose(false);
        }
    }
}
