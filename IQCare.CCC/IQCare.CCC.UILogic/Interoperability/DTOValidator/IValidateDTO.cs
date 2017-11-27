using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Interoperability.DTOValidator
{
    public interface IValidateDTO <T> where T : class
    {
        string ValidateDTO(T t);
    }
}
