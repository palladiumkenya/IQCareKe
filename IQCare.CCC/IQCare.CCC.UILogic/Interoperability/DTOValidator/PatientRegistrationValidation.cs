using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.CommonEntities;
using IQCare.DTO.PatientRegistration;

namespace IQCare.CCC.UILogic.Interoperability.DTOValidator
{
    public class PatientRegistrationValidation : IValidateDTO<PatientRegistrationDTO>
    {
        public string ValidateDTO(PatientRegistrationDTO entity)
        {
            string errors = String.Empty;
            var context = new ValidationContext(entity, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, context, result, true);
            if (!isValid)
            {
                errors = PrintResults(result);
            }
            return errors;
        }

        private static string PrintResults(IEnumerable<ValidationResult> results)
        {
            string errors = String.Empty;
            foreach (var validationResult in results)
            {

                Console.WriteLine(validationResult.ErrorMessage + "\n");
                errors += (validationResult.ErrorMessage + "\n");

                if (validationResult is CompositeValidationResult)
                {
                    errors += (PrintResults(((CompositeValidationResult)validationResult).Results)) + "\n";
                }
            }

            return errors;
        }
    }
}
