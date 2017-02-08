using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientAppointmentRepository : BaseRepository<PatientAppointment>, IPatientAppointmentRepository
    {
        public PatientAppointmentRepository(GreencardContext context) : base(context)
        {
        }

        public PatientAppointmentRepository() : this(new GreencardContext())
        {
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
    }
}