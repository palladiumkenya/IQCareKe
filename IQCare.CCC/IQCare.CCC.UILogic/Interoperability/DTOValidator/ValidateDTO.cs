using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Interoperability.DTOValidator
{
    public class ValidateDTO
    {
        public static List<ValidationResult> validateDTO<T>(T entity)
        {
            var context = new ValidationContext(entity, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, context, result, true);

            return result;
        }
    }
}
