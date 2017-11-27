using System;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class OBSERVATIONRESULT
    {
        public string SET_ID { get; set; }
        public string OBSERVATION_IDENTIFIER { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string VALUE_TYPE { get; set; }
        public string OBSERVATION_VALUE { get; set; }
        public string UNITS { get; set; }
        public string OBSERVATION_RESULT_STATUS { get; set; }
        public DateTime OBSERVATION_DATETIME { get; set; }
        public string ABNORMAL_FLAGS { get; set; }
    }

    public class PatientRegistrationEntity
    {
        public PatientRegistrationEntity()
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