using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Consent;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientConsentRepository : BaseRepository<PatientConsent>, IPatientConsentRepository,IDisposable
    {
        private GreencardContext _context;

        public PatientConsentRepository() : this(new GreencardContext())
        {
        }

        public PatientConsentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            IPatientConsentRepository patientConsentRepository = new PatientConsentRepository();
            List<PatientConsent> patientConsent = patientConsentRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientConsent;
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

        ~PatientConsentRepository()
        {
            Dispose(false);
        }
    }
}