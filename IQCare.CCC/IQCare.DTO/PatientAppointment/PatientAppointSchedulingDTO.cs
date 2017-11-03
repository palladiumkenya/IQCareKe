using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.CommonEntities;

namespace IQCare.DTO.PatientAppointment
{
    public class PatientAppointSchedulingDTO
    {
        public PatientAppointSchedulingDTO()
        {
            MESSAGE_HEADER = new MESSAGEHEADER();
            PATIENT_IDENTIFICATION = new PATIENTIDENTIFICATION();
            APPOINTMENT_INFORMATION = new List<APPOINTMENT_INFORMATION>();
        }

        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<APPOINTMENT_INFORMATION> APPOINTMENT_INFORMATION { get; set; }
    }
}
