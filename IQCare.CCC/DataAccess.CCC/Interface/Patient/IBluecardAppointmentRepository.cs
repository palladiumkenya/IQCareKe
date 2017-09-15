using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IBluecardAppointmentRepository : IRepository<BlueCardAppointment>
    {
        List<BlueCardAppointment> GetBluecardPatientAppointmentsBypatientId(int patientId);
    }
}
