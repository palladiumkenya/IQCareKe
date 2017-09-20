using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Web.MessageProcessing.JsonMappingEntities
{
    public class MESSAGEHEADER
    {
        public string SENDING_APPLICATION { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public DateTime MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }
    }

    public class EXTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class INTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class PATIENTNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    public class PHYSICALADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
    }

    public class PATIENTADDRESS
    {
        public PHYSICALADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }
    }

    public class PATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
        public string MOTHER_MAIDEN_NAME { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public string SEX { get; set; }
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public DateTime DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }
    }

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
        public DateTime DATE_OF_BIRTH { get; set; }
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
