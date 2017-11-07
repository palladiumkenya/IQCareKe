using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class PatientAppointmentEntity
    {
        public PatientAppointmentEntity()
        {
            MESSAGE_HEADER = new MESSAGEHEADER();
            PATIENT_IDENTIFICATION = new APPOINTMENTPATIENTIDENTIFICATION();
            APPOINTMENT_INFORMATION = new List<APPOINTMENT_INFORMATION>();
        }

        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public APPOINTMENTPATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<APPOINTMENT_INFORMATION> APPOINTMENT_INFORMATION { get; set; }
    }
}
