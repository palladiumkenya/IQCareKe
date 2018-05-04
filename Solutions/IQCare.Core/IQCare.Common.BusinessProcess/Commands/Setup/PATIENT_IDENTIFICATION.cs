using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class PATIENT_IDENTIFICATION
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENT_NAME PATIENT_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public int SEX { get; set; }
        public List<int> KEY_POP { get; set; }
        public PATIENT_ADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public int MARITAL_STATUS { get; set; }
        public string REGISTRATION_DATE { get; set; }
    }

    public class PATIENT_ADDRESS
    {
        public PHYSICAL_ADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }
    }

    public class PHYSICAL_ADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }
    }

    public class PATIENT_NAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    public class INTERNAL_PATIENT_ID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class PARTNER_FAMILY_PATIENT_IDENTIFICATION : PATIENT_IDENTIFICATION
    {
        public int RELATIONSHIP_TYPE { get; set; }
    }
}