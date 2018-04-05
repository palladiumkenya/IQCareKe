using Entity.WebApi.PSmart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.WebApi
{
    public interface IPSmartAuthManager
    {
        PsmartAuthUser LoginValidate(string username);
    }
}
