using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.DTO.CommonEntities
{
    public class CommonEntities
    {

    }

    public class VISIT
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Visit Date Is Required")]
        public string VISIT_DATE { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Type Is Required")]
        public string PATIENT_TYPE { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Source Is Required")]
        public string PATIENT_SOURCE { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enrollment Date Is Required")]
        public string HIV_CARE_ENROLLMENT_DATE { get; set; }
    }

    public class NEXTOFKIN
    {
        public NEXTOFKIN()
        {
            NOK_NAME = new NOKNAME();
        }
        [Required, ValidateObject]
        public NOKNAME NOK_NAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }
    }

    public class NOKNAME
    {
        [Required(AllowEmptyStrings = false)]
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LAST_NAME { get; set; }
    }

    public abstract class PatientBaseProperties
    {
        public PATIENTNAME MOTHER_NAME { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Of Birth Is Required")]
        public string DATE_OF_BIRTH { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of Birth Precision Is Required")]
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex Is Required")]
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
        [Required, ValidateObject]
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        [Required, ValidateObject]
        public PATIENTNAME PATIENT_NAME { get; set; }
    }

    public class APPOINTMENTPATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        [Required, ValidateObject]
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }

    public class PATIENTADDRESS
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
        public string NEAREST_LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }
    }

    public class PATIENTNAME
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName Is Required")]
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName Is Required")]
        public string LAST_NAME { get; set; }
    }

    public class EXTERNALPATIENTID
    {
        public string ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identifier Type Is Required")]
        public string IDENTIFIER_TYPE { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Assigning Authority Is Required")]
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class INTERNALPATIENTID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ID Is Required")]
        public string ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identifier Type Is Required")]
        public string IDENTIFIER_TYPE { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Assigning Authority is Required")]
        public string ASSIGNING_AUTHORITY { get; set; }
    }

    public class MESSAGEHEADER
    {
        public string SENDING_APPLICATION { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sending Facility Is Required")]
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public string MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message Type Is Required")]
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }
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

        public string APPOINTMENT_STATUS { get; set; }
        public string APPOINTMENT_HONORED { get; set; }
    }

    public class ORDERINGPHYSICIAN
    {
        public OrderingPysicianDto ORDERING_PHYSICIAN { get; set; }
    }

    public class PLACER_APPOINTMENT_NUMBER
    {
        public string NUMBER { get; set; }
        public string ENTITY { get; set; }
    }

    public class OBSERVATIONPATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
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


    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);

            if (results.Count != 0)
            {
                var compositeResults = new CompositeValidationResult(String.Format("Validation for {0} failed!", validationContext.DisplayName));
                results.ForEach(compositeResults.AddResult);

                return compositeResults;
            }

            return ValidationResult.Success;
        }
    }

    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return _results;
            }
        }

        public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public void AddResult(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }
    }
}
