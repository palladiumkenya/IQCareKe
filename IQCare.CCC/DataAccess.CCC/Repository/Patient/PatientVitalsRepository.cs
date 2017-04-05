using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientVitalsRepository : BaseRepository<PatientVital>, IPatientVitalsRepository,IDisposable
    {
        private readonly GreencardContext _context;

        public PatientVitalsRepository() : this(new GreencardContext())
        {
        }

        public PatientVitalsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public PatientVital GetByPatientId(int patientId)
        {
            IPatientVitalsRepository patientVitalsRepository = new PatientVitalsRepository();
            PatientVital patientVital = patientVitalsRepository.FindBy(p => p.PatientId == patientId).FirstOrDefault();
            return patientVital;
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

        ~PatientVitalsRepository()
        {
            Dispose(false);
        }
    }
}