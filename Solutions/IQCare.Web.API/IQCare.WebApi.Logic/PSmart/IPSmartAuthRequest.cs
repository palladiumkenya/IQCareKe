using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.PSmart;

namespace IQCare.WebApi.Logic.PSmart
{
    public interface IPSmartAuthRequest
    {
        DtoUserAuth Authentication(string username, string password);
    }
}
