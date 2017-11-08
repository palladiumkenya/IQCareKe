using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.CommonEntities;
using System.ComponentModel.DataAnnotations;

namespace IQCare.DTO.PatientRegistration
{
    public class PatientRegistrationDTO
    {
        public PatientRegistrationDTO()
        {
            MESSAGE_HEADER = new MESSAGEHEADER();
            PATIENT_IDENTIFICATION = new PATIENTIDENTIFICATION();
            NEXT_OF_KIN = new List<NEXTOFKIN>();
            PATIENT_VISIT = new VISIT();
        }

        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; }
        public VISIT PATIENT_VISIT { get; set; }
    }
}
