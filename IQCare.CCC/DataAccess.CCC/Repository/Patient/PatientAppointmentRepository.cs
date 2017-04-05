using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientAppointmentRepository : BaseRepository<PatientAppointment>, IPatientAppointmentRepository,IDisposable
    {
        private GreencardContext _context;

        public PatientAppointmentRepository() : this(new GreencardContext())
        {
        }

        public PatientAppointmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            IPatientAppointmentRepository patientAppointmentRepository = new PatientAppointmentRepository();
            List<PatientAppointment> patientAppointment = patientAppointmentRepository.FindBy(p => p.PatientId == patientId).ToList();
            return patientAppointment;
        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            IPatientAppointmentRepository patientAppointmentRepository = new PatientAppointmentRepository();
            List<PatientAppointment> patientAppointment = patientAppointmentRepository.FindBy(p => p.AppointmentDate == date).ToList();
            return patientAppointment;
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            IPatientAppointmentRepository patientAppointmentRepository = new PatientAppointmentRepository();
            List<PatientAppointment> patientAppointment = patientAppointmentRepository.FindBy(p => p.AppointmentDate >= startDate && p.AppointmentDate<= endDate).ToList();
            return patientAppointment;
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

        ~PatientAppointmentRepository()
        {
            Dispose(false);
        }
    }
}