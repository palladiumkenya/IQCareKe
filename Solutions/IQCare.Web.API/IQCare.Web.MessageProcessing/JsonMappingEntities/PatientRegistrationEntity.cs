using System;
using System.Collections.Generic;

namespace IQCare.Web.MessageProcessing.JsonMappingEntities
{
    public class NOKNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    public class NEXTOFKIN
    {
        public NOKNAME NOK_NAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public DateTime ? DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }
    }

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
        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; }
        public List<OBSERVATIONRESULT> OBSERVATION_RESULT { get; set; }
    }
}