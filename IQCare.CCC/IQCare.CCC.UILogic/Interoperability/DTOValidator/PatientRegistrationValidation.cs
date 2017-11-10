using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.PatientRegistration;

namespace IQCare.CCC.UILogic.Interoperability.DTOValidator
{
    public class PatientRegistrationValidation : IValidateDTO<PatientRegistrationDTO>
    {
        public List<ValidationResult> ValidateDTO(PatientRegistrationDTO entity)
        {
            var context = new ValidationContext(entity, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, context, result, true);

            return result;
        }
    }
}
