using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class PATIENT_IDENTIFICATION
    {
        [Required, ValidateObject]
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        [Required, ValidateObject]
        public PATIENT_NAME PATIENT_NAME { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string DATE_OF_BIRTH { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        [Required, Range(1, Double.MaxValue)]
        public int SEX { get; set; }
        [EnsureMinimumElements(1, ErrorMessage = "At least one key pop is required")]
        public List<int> KEY_POP { get; set; }
        [Required, ValidateObject]
        public PATIENT_ADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        [Required, Range(1, Double.MaxValue)]
        public int MARITAL_STATUS { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string REGISTRATION_DATE { get; set; }
        [Required]
        public int USER_ID { get; set; }


        public string EDUCATIONLEVEL { get; set; }

        public string EDUCATIONOUTCOME { get; set; }

        public string OCCUPATION { get; set; }
    }

    public class PATIENT_ADDRESS
    {
        [Required, ValidateObject]
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
        [Required(AllowEmptyStrings = false)]
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LAST_NAME { get; set; }

        public string NICK_NAME { get; set; }
    }

    public class INTERNAL_PATIENT_ID
    {
        [Required(AllowEmptyStrings = false)]
        public string ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string IDENTIFIER_TYPE { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class PARTNER_FAMILY_PATIENT_IDENTIFICATION : PATIENT_IDENTIFICATION
    {
        public int RELATIONSHIP_TYPE { get; set; }
    }
}