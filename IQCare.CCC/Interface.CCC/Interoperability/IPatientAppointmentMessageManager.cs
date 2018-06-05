using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Interoperability;

namespace Interface.CCC.Interoperability
{
    public interface IPatientAppointmentMessageManager
    {
        PatientAppointmentMessage GetPatientAppointmentMessageById(int appointmentId);
    }
}
