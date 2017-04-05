using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientLookupRepository:BaseRepository<PatientLookup>,IPatientLookupRepository,IDisposable
    {
        private readonly LookupContext _context;

        public PatientLookupRepository():this(new LookupContext())
        {
            
        }

        public PatientLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

        public PatientLookup GetGenderId(int patientId)
        {
            IPatientLookupRepository patientRepository = new PatientLookupRepository();
            var genderId = patientRepository.FindBy(x => x.Id == patientId).FirstOrDefault();
            return genderId;

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
