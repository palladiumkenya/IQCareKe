using System;
using System.Collections.Generic;
using IQCare.DTO;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class MESSAGEHEADER
    {
        public string SENDING_APPLICATION { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public string MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }
    }

    public class VISIT
    {
        public string VISIT_DATE { get; set; }
        public string PATIENT_TYPE { get; set; }
        public string PATIENT_SOURCE { get; set; }
        public string HIV_CARE_ENROLLMENT_DATE { get; set; }
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
        public string NEAREST_LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }
    }

    public class PATIENTADDRESS
    {
        public PHYSICALADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }
    }

    public abstract class PatientBaseProperties
    {
        public PATIENTNAME MOTHER_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public string SEX { get; set; }
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }
    }

    public class PATIENTIDENTIFICATION : PatientBaseProperties
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; } 
    }

    public class APPOINTMENTPATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }

    public class OBSERVATIONPATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }

    public class NOKNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    public class NEXTOFKIN
    {
        public NEXTOFKIN()
        {
            NOK_NAME = new NOKNAME();
        }

        public NOKNAME NOK_NAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }
    }

    public class APPOINTMENT_INFORMATION
    {
        public PLACER_APPOINTMENT_NUMBER PLACER_APPOINTMENT_NUMBER { get; set; }

        public string APPOINTMENT_REASON { get; set; }

        public string APPOINTMENT_TYPE { get; set; }

        public string APPOINTMENT_DATE { get; set; }

        public string APPOINTMENT_PLACING_ENTITY { get; set; }

        public string APPOINTMENT_LOCATION { get; set; }

        public string ACTION_CODE { get; set; }

        public string APPOINTMENT_NOTE { get; set; }

        public string APPOINTMENT_HONORED { get; set; }
    }

    public class PLACER_APPOINTMENT_NUMBER
    {
        public string NUMBER { get; set; }
        public string ENTITY { get; set; }
    }

    public class OBSERVATION_RESULT
    {
        public string OBSERVATION_IDENTIFIER { get; set; }
        public string OBSERVATION_SUB_ID { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string VALUE_TYPE { get; set; }
        public string OBSERVATION_VALUE { get; set; }
        public string UNITS { get; set; }
        public string OBSERVATION_RESULT_STATUS { get; set; }
        public string OBSERVATION_DATETIME { get; set; }
        public string ABNORMAL_FLAGS { get; set; }
    }
}