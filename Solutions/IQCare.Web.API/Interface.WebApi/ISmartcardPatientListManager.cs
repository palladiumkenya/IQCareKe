using System.Collections.Generic;
using Entity.WebApi.PSmart;

namespace Interface.WebApi
{
    public interface ISmartcardPatientListManager
    {
     List<PsmartEligibleList>  GetSmartCardPatientList();
    }
}