using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Repository.Patient
{
    public class BluecardAppointmentRepository : BaseRepository<BlueCardAppointment>, IBluecardAppointmentRepository
    {
        private GreencardContext _context;

        public BluecardAppointmentRepository() : this(new GreencardContext())
        {

        }

        public BluecardAppointmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<BlueCardAppointment> GetBluecardPatientAppointmentsBypatientId(int patientId)
        {
            IBluecardAppointmentRepository bluecardAppointmentRepository = new BluecardAppointmentRepository();
            var blueCardAppointments = bluecardAppointmentRepository.FindBy(p => p.PatientId == patientId).ToList();
            return blueCardAppointments;
        }
    }
}
