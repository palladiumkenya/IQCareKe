
using Entity.WebApi.PSmart;
using System.Collections.Generic;

namespace Interface.WebApi
{
    public interface ISmartcardPatientListManager
    {
        List<PsmartEligibleList>  GetSmartCardPatientList();
    }
}