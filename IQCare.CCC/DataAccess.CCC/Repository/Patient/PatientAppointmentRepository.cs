using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientAppointmentRepository : BaseRepository<PatientAppointment>, IPatientAppointmentRepository
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
            List<PatientAppointment> patientAppointment = patientAppointmentRepository.FindBy(p => DbFunctions.TruncateTime(p.AppointmentDate) == DbFunctions.TruncateTime(date)).ToList();
            return patientAppointment;
        }
        public List<AppointmentSummary> GetAppointmentSummaryByDate(DateTime date)
        {
         //   IPatientAppointmentRepository patientAppointmentRepository = new PatientAppointmentRepository();
           return _context.AppointmentSummary.Where(p => DbFunctions.TruncateTime(p.AppointmentDate) == DbFunctions.TruncateTime(date)).ToList();

        }
        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            IPatientAppointmentRepository patientAppointmentRepository = new PatientAppointmentRepository();
            List<PatientAppointment> patientAppointment = patientAppointmentRepository
                .FindBy(p => p.AppointmentDate >= DbFunctions.TruncateTime(startDate) && p.AppointmentDate<= DbFunctions.TruncateTime(endDate)).ToList();
            return patientAppointment;
        }
       
    }
}