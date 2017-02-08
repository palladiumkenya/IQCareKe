using System;
using System.Collections.Generic;
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

        public PatientAppointment GetByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}